using System;
using System.Collections.Generic;
using System.Text;

namespace WCBookMigrator.Models
{
    ///<summary>
    ///The section of the book. e.g. Public Worship, Nativity of Christ
    public class BookSection : BaseModel
    {
        public string Name { get; set; }
        public BookPart Part { get; set; }
        public string BookPartId { get; set; }
    }
}
