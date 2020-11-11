using System;
using System.Collections.Generic;
using System.Text;

namespace WCBookMigrator.Models.DTOs
{
    public class SongSectionDTO : BaseModel
    {
        public string Type { get; set; }
        public string SongId { get; set; }
        public int Order { get; set; }
    }
}
