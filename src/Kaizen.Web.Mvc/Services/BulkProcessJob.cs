using Abp.Application.Services.Dto;
using Abp.BackgroundJobs;
using Abp.Dependency;
using Kaizen.Entities.SupportTypes.Dto;
using Kaizen.EntityFrameworkCore;
using Kaizen.Enums;
using Kaizen.Web.Models.BulkProcess;
using System;
using System.Linq;

namespace Kaizen.Web.Services
{
    public class BulkProcessJob<T> : BackgroundJob<BulkProcessJobArgs<T>>, ITransientDependency
        where T : EntityDto<long>
    {
        public KaizenDbContext _context { get; set; }

        public BulkProcessJob(KaizenDbContext context)
        {
            _context = context;
        }

        public override void Execute(BulkProcessJobArgs<T> args)
        {
            try
            {
                // Read in the CSV
                _context.BulkProcessJob.Add(new Entities.BulkProcess.BulkProcessJob
                {
                    BulkProcessStatus = BulkProcessStatus.Processing,
                    CreatedByUserId = args.CreatedByUserId,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    BulkProcessType = args.BulkProcessType,
                    FileUploadedUrl = string.Empty // Need this to go to blob storage
                });

                _context.SaveChanges();

                foreach (var record in args.FileResults.ToList())
                {
                    // Will need to do more complex validation on other jobs.
                    SupportTypeDto dbModel = record.BulkProcessItem as SupportTypeDto;

                    _context.SupportType.Add(new Entities.SupportTypes.SupportType
                    {
                        Name = dbModel.Name,
                        Description = dbModel.Description,
                        Cost = dbModel.Cost,
                        Margin = dbModel.Margin
                    });

                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
