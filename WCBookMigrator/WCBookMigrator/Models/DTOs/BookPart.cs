using System;
using System.Collections.Generic;
using System.Text;

namespace WCBookMigrator.Models.DTOs
{
    ///<summary>
    ///The part of the book. e.g. Main, Appendix
    ///</summary>
    public class BookPartDTO : BaseModel
    {
        public string Name { get; set; }
    }
}
