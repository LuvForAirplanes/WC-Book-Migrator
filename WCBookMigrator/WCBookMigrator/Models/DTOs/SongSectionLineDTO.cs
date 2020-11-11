using System;
using System.Collections.Generic;
using System.Text;

namespace WCBookMigrator.Models.DTOs
{
    public class SongSectionLineDTO : BaseModel
    {
        public int Order { get; set; }
        public string Line { get; set; }
        public string SongSectionId { get; set; }
    }
}
