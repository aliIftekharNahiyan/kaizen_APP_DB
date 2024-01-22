using Abp.Domain.Entities;

namespace Kaizen.Enums
{
    public enum BulkProcessStatus
    {
        Processing = 0,
        Completed = 1,
        CompletedWithErrors = 2
    }
}
