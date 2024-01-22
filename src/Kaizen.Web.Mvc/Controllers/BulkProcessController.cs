using Abp.Runtime.Security;
using CsvHelper;
using CsvHelper.Configuration;
using Hangfire;
using Kaizen.Controllers;
using Kaizen.Entities.BulkProcesses.Dto;
using Kaizen.Entities.SupportTypes.Dto;
using Kaizen.EntityFrameworkCore;
using Kaizen.Enums;
using Kaizen.Web.Models.BulkProcess;
using Kaizen.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen.Web.Controllers
{
    //[AbpMvcAuthorize(PermissionNames.Pages_Addresses)]
    public class BulkProcessController : KaizenControllerBase
    {
        private readonly KaizenDbContext _context;

        public BulkProcessController(KaizenDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> DownloadTemplate(BulkProcessType bulkProcessType)
        {
            switch (bulkProcessType)
            {
                case BulkProcessType.SupportType:
                    var memoryStream = new MemoryStream();

                    var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8, 1024, true);

                    using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                    {
                        csvWriter.WriteHeader<SupportTypeBulkProcessDto>();
                    }

                    memoryStream.Position = 0;

                    return new FileStreamResult(memoryStream, "text/csv") { FileDownloadName = bulkProcessType.ToString() + "Create.csv" };

                    break;
            }

            return BadRequest();
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> StartProcess(BulkProcessViewModel data)
        {
            List<string> errorLines = new List<string>();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                ReadingExceptionOccurred = args =>
                {
                    errorLines.Add(args.Exception.Context.Parser.Row +
                                   "\r\n" + (args.Exception.Context.Reader.HeaderRecord[args.Exception.Context.Reader.CurrentIndex]) + " (" + (args.Exception.Context.Reader.CurrentIndex + 1) + ")" +
                                   "\r\n" + args.Exception.Context.Parser.Record[args.Exception.Context.Reader.CurrentIndex]);

                    return false;
                }
            };

            try
            {
                List<BulkProcessTypeDto<SupportTypeDto>> bulkProcessItems = new List<BulkProcessTypeDto<SupportTypeDto>>();

                using (var reader = new StreamReader(data.UploadedFile.OpenReadStream()))
                {
                    switch (data.BulkProcessType)
                    {
                        case BulkProcessType.SupportType:
                            using (var csv = new CsvReader(reader, config))
                            {
                                IEnumerable<SupportTypeBulkProcessDto> records = csv.GetRecords<SupportTypeBulkProcessDto>();

                                foreach (var record in records.ToList())
                                {
                                    BulkProcessTypeDto<SupportTypeDto> bulkItem = new BulkProcessTypeDto<SupportTypeDto>();

                                    bulkItem.BulkProcessItem = new SupportTypeDto
                                    {
                                        Name = record.Name,
                                        Description = record.Description,
                                        Cost = record.Cost,
                                        Margin = record.Margin
                                    };

                                    bulkProcessItems.Add(bulkItem);
                                }
                            }


                            if (errorLines.Count == 0)
                            {
                                BackgroundJob.Enqueue<BulkProcessJob<SupportTypeDto>>(a => a.Execute(new BulkProcessJobArgs<SupportTypeDto>
                                {
                                    BulkProcessType = data.BulkProcessType,
                                    FileResults = bulkProcessItems,
                                    TableId = data.TableId,
                                    CreatedByUserId = User.Identity.GetUserId().Value
                                }));

                                break;
                            }
                            else
                            {
                                return Json(new
                                {
                                    success = false,
                                    message = errorLines
                                });
                            }
                    }

                }

                return Json(new
                {
                    success = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }


}
