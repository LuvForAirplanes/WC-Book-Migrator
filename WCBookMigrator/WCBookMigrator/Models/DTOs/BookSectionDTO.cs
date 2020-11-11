using System;
using System.Collections.Generic;
using System.Text;

namespace WCBookMigrator.Models.DTOs
{
    public class BookSectionDTO : BaseModel
    {
        public string Name { get; set; }
        public string BookPartId { get; set; }
    }
}
