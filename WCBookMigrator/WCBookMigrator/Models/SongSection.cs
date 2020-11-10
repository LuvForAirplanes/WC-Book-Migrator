using System;
using System.Collections.Generic;
using System.Text;

namespace WCBookMigrator.Models
{
    public class SongSection : BaseModel
    {
        ///<summary>
        ///The type of section. The two valid values are Verse and Chorus.
        ///</summary>
        public string Type { get; set; }
        public Song Song { get; set; }
        public string SongId { get; set; }
        ///<summary>
        ///List of lines.
        ///</summary>
        public List<SongSectionLine> Lines { get; set; }
    }
}
