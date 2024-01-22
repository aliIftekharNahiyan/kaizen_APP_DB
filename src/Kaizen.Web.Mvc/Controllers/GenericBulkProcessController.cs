using Abp.Runtime.Security;
using CsvHelper;
using CsvHelper.Configuration;
using Hangfire;
using Kaizen.Controllers;
using Kaizen.Entities.FundingBodys;
using Kaizen.Entities.FundingBodys.Dto;
using Kaizen.Entities.GenericBulkProcesses.Dto;
using Kaizen.Entities.SessionGroups;
using Kaizen.Entities.SessionGroups.Dto;
using Kaizen.Entities.SupportTypes;
using Kaizen.Entities.SupportTypes.Dto;
using Kaizen.Enums;
using Kaizen.Users;
using Kaizen.Users.Dto;
using Kaizen.Web.Models.GenericBulkProcess;
using Kaizen.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen.Web.Controllers
{
    //[AbpMvcAuthorize(PermissionNames.Pages_Addresses)]
    public class GenericBulkProcessController : KaizenControllerBase
    {
       
        private readonly IFundingBodyAppService _fundingBodyAppService;
        private readonly ISupportTypeAppService _supportTypeAppService;
        private readonly IUserAppService _userAppService;

        public GenericBulkProcessController( IFundingBodyAppService fundingBodyAppService, ISupportTypeAppService supportTypeAppService,IUserAppService userAppService)
        {
            _fundingBodyAppService = fundingBodyAppService;
            _supportTypeAppService= supportTypeAppService;
            _userAppService= userAppService;

        }

        public async Task<ActionResult> DownloadTemplate(BulkProcessType bulkProcessType)
        {
            switch (bulkProcessType)
            {
                case BulkProcessType.SessionGroup:
                    var memoryStream = new MemoryStream();

                    var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8, 1024, true);

                    using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                    {
                        csvWriter.WriteHeader<SessionGroupViewModel>();
                    }

                    memoryStream.Position = 0;

                    return new FileStreamResult(memoryStream, "text/csv") { FileDownloadName = bulkProcessType.ToString() + "Create.csv" };

                   
            }

            return BadRequest();
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> StartProcess(GenericBulkProcessViewModel data)
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
                
                //if ( data.BulkProcessType.SessionGroup == GenricBulkProcessType.SessionGroup)

                List<GenericBulkProcessTypeDto<SessionGroupDto>> bulkProcessItems = new List<GenericBulkProcessTypeDto<SessionGroupDto>>();

                using (var reader = new StreamReader(data.UploadedFile.OpenReadStream()))
                {
                    switch (data.BulkProcessType)
                    {
                        case BulkProcessType.SessionGroup:
                            using (var csv = new CsvReader(reader, config))
                            {
                                IEnumerable<SessionGroupViewModel> records = csv.GetRecords<SessionGroupViewModel>();
                                
                                // datavalidation
                                var vData = records.ToList();
                                var isValid = await SessionGroupDataValidate(vData, errorLines);

                                if (isValid)
                                {

                                    foreach (var record in vData)
                                    {
                                        GenericBulkProcessTypeDto<SessionGroupDto> bulkItem = new GenericBulkProcessTypeDto<SessionGroupDto>();

                                        bulkItem.GenericBulkProcessItem = new SessionGroupDto
                                        {
                                            Name = record.Name,
                                            Description = record.Description,
                                            SupportTypeId = record.SupportTypeId,
                                            FundingBodyId = record.FundingBodyId,
                                            ExpiryDate = record.ExpiryDate,
                                            AllocatedHours = record.AllocatedHours,
                                            AllocatedBudget = record.AllocatedBudget,
                                            Deleted = record.Deleted,
                                            UserId = User.Identity.GetUserId().Value
                                        };

                                        bulkProcessItems.Add(bulkItem);
                                    }
                                }
                            
                            }


                            if (errorLines.Count == 0)
                            {
                                BackgroundJob.Enqueue<GenericBulkProcessJob<SessionGroup, SessionGroupDto>>(a => a.Execute(new GenericBulkProcessJobArgs<SessionGroupDto>
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

        #region Data Validation
        // session Group Validation : for data 
        private async Task<bool> SessionGroupDataValidate(List<SessionGroupViewModel> sessionGroupDtos,List<string> errorLines)
        {
            bool isSuccess = true;


            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(sessionGroupDtos, serviceProvider: null, items: null);

            bool isValid = Validator.TryValidateObject(sessionGroupDtos, validationContext, validationResults, validateAllProperties: true);

            if (!isValid)
            {
                foreach (var validationResult in validationResults)
                {
                    errorLines.Add($"Validation Error: \r\n {validationResult.ErrorMessage}");
                }
                return isSuccess = false;
            }


            var funds = (await _fundingBodyAppService.GetAllAsync(new PagedFundingBodyResultRequestDto() { MaxResultCount =int.MaxValue})).Items;
            var supportTypes = (await _supportTypeAppService.GetAllAsync(new PagedSupportTypeResultRequestDto() { MaxResultCount = int.MaxValue })).Items;
            var users = (await _userAppService.GetAllAsync(new PagedUserResultRequestDto() { MaxResultCount = int.MaxValue })).Items;           


            var notFundingBody = sessionGroupDtos.Where(p => !funds.Any(e => e.Id == p.FundingBodyId)).ToList();
            if (notFundingBody.Any())
            {
                foreach (var fund in notFundingBody)
                {               

                    errorLines.Add(sessionGroupDtos.FindIndex(p => p.Name == fund.Name && p.FundingBodyId == fund.FundingBodyId) +
                                   "\r\n" + ("FundingBodyId" ) + " (" + (GetPropertyIndex(fund, "FundingBodyId") + 1) + ")" +
                                   "\r\n" + fund.FundingBodyId);
                }
                isSuccess = false;
            }

            var NotsupportTypes = sessionGroupDtos.Where(p => !supportTypes.Any(e => e.Id == p.SupportTypeId)).ToList();
            if (notFundingBody.Any())
            {
                foreach (var st in NotsupportTypes)
                {
                    errorLines.Add(sessionGroupDtos.FindIndex(p => p.Name == st.Name && p.SupportTypeId == st.SupportTypeId) +
                                 "\r\n" + ("SupportTypeId") + " (" + (GetPropertyIndex(st, "SupportTypeId")+1) + ")" +
                                 "\r\n" + st.SupportTypeId);
                }
                isSuccess = false;
            }

            var notUser = sessionGroupDtos.Where(p => !users.Any(e => e.EmailAddress == p.UserId)).ToList();

            if (notUser.Any())
            {
                foreach (var user in notUser)
                {
                    errorLines.Add(sessionGroupDtos.FindIndex(p => p.Name == user.Name && p.UserId == user.UserId) +
                                "\r\n" + ("UserId") + " (" + (GetPropertyIndex(user, "UserId")+1) + ")" +
                                "\r\n" + user.UserId);
                }
                isSuccess = false;
            }

            return isSuccess;            
        }

        private int GetPropertyIndex(object obj, string propertyName)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                if (properties[i].Name == propertyName)
                {
                    return i;
                }
            }

            // Property not found
            return -1;
        }

        #endregion
    }


}
