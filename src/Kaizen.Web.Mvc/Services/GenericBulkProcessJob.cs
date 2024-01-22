using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Repositories;
using AutoMapper;
using Kaizen.Entities.BulkProcess;
using Kaizen.Enums;
using Kaizen.Web.Models.GenericBulkProcess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kaizen.Web.Services
{
    public class GenericBulkProcessJob<IE, T> : BackgroundJob<GenericBulkProcessJobArgs<T>>, ITransientDependency where IE : class, IEntity<long>
        where T : EntityDto<long>
    {
      
        private IRepository<IE, long> _uploadRepository { get; }
        private IRepository<BulkProcessJob, long> _bulkRepository { get; }

     
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IMapper _mapper;
        public GenericBulkProcessJob(IRepository<IE, long> uploadRepository, IRepository<BulkProcessJob, long> bulkRepository, IUnitOfWorkManager unitOfWorkManager, IMapper mapper)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _uploadRepository = uploadRepository;
            _bulkRepository = bulkRepository;
            _mapper = mapper;
        }

       // [UnitOfWork]
        public override void Execute(GenericBulkProcessJobArgs<T> args)
        {
            try
            {


                using (var unitOfWork = _unitOfWorkManager.Begin())
                {
                    _bulkRepository.Insert(new BulkProcessJob
                    {
                        BulkProcessStatus = BulkProcessStatus.Processing,
                        CreatedByUserId = args.CreatedByUserId,
                        DateCreated = DateTime.Now,
                        DateUpdated = DateTime.Now,
                        BulkProcessType = args.BulkProcessType,
                        FileUploadedUrl = string.Empty // Need this to go to blob storage
                    });

                    unitOfWork.Complete();

                }
                using (var unitOfWork = _unitOfWorkManager.Begin())
                {
                    if (args.FileResults.Count() > 0)
                    {

                        var list = args.FileResults.ToList().Select(s => s.GenericBulkProcessItem).ToList();
                        var ie = new List<IE>();
                        _mapper.Map(list, ie);
                        _uploadRepository.InsertRange(ie);

                        unitOfWork.Complete();
                        args.FileResults.Clear();
                    }
                   
                }

            }
            catch (Exception )
            {
                throw ;
            }
        }
    }
}
