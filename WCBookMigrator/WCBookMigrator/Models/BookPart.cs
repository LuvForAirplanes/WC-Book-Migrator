using System;
using System.Collections.Generic;
using System.Text;

namespace WCBookMigrator.Models
{
    ///<summary>
    ///The part of the book. e.g. Main, Appendix
    ///</summary>
    public class BookPart : BaseModel
    {
        public string Name { get; set; }
    }
}
