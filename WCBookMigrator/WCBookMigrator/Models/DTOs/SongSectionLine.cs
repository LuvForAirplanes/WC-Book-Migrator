using System;
using System.Collections.Generic;
using System.Text;

namespace WCBookMigrator.Models
{
    public class SongSectionLine : BaseModel
    {
        ///<summary>
        ///The order in which this line comes in the song section.
        ///</summary>
        public int Order { get; set; }
        ///</summary>
        ///The actual song line.
        ///</summary>
        public string Line { get; set; }
        public SongSection SongSection { get; set; }
        public string SongSectionId { get; set; }
    }
}
