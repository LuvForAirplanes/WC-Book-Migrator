using System;
using System.Collections.Generic;
using System.Text;

namespace WCBookMigrator.Models.DTOs
{
    public class SongDTO : BaseModel
    {
        public int Number { get; set; }
        public string Title { get; set; }
        public string TuneId { get; set; }
        public string BookSectionId { get; set; }
        public string MeterId { get; set; }
        public int BeginsPage { get; set; }
        public int EndsPage { get; set; }
    }
}
