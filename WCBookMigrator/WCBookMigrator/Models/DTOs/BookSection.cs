using System;
using System.Collections.Generic;
using System.Text;

namespace WCBookMigrator.Models.DTOs
{
    ///<summary>
    ///The section of the book. e.g. Public Worship, Nativity of Christ
    public class BookSectionDTO : BaseModel
    {
        public string Name { get; set; }
        public string BookPartId { get; set; }
    }
}
