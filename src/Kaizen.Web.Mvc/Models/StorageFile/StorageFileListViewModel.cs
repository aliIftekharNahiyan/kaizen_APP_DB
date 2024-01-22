
using Kaizen.Entities.StorageFiles.Dto;
using System.Collections.Generic;

namespace Kaizen.Web.Models.StorageFiles
{
    public class StorageFileListViewModel
    {
        public IReadOnlyList<StorageFileDto> Entities { get; set; }
    }
}