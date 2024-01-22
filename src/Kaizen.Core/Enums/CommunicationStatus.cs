using Abp.Domain.Entities;

namespace Kaizen.Enums
{
    public enum CommunicationStatus
    {
        Created = 0,
        Processing = 1,
        Completed = 2,
        CompletedWithErrors = 3
    }
}
