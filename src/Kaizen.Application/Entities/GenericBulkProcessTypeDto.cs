namespace Kaizen.Entities.GenericBulkProcesses.Dto
{
    public class GenericBulkProcessTypeDto<T> where T : class
    {
        public T GenericBulkProcessItem { get; set; }
    }
}

