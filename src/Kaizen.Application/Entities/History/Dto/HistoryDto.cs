using System;

namespace Kaizen.Entities.History.Dto
{
    public class HistoryDto
    {
       public string NewValue { get; set; }
       public string OriginalValue { get; set; }
       public string PropertyName { get; set; }
    }
}

