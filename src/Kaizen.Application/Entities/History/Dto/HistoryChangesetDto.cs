using System;

namespace Kaizen.Entities.History.Dto
{
    public class HistoryChangesetDto
    {
        public long Id { get; set; }
        public string CreationTime { get; set; }
        public string PersonName { get; set; }
        public long UserId { get; set; }
        public string ChangeType { get; set; }
    }
}

