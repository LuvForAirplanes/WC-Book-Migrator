using System;
using System.Collections.Generic;
using System.Text;

namespace WCBookMigrator.Models
{
    public class Song : BaseModel
    {
        ///<summary>
        ///The number of the song in the Mennonite Hymns book.
        ///</summary>
        public int Number { get; set; }
        ///<summary>
        ///The title of the song.
        ///</summary>
        public string Title { get; set; }
        ///<summary>
        ///The tune that preferably sung with this song.
        ///</summary>
        public Tune Tune { get; set; }
        public string TuneId { get; set; }
        ///<summary>
        ///The section this song is in. e.g. Public Worship
        ///</summary>
        public BookSection Section { get; set; }
        public string BookSectionId { get; set; }
        ///<summary>
        ///The meter this song is written in.
        ///</summary>
        public Meter Meter { get; set; }
        public string MeterId { get; set; }
        ///<summary>
        ///The page this song begins on in the Mennonite Hymns book.
        ///</summary>
        public int BeginsPage { get; set; }
        ///<summary>
        ///The page this song ends on in the Mennonite Hymns book.
        ///</summary>
        public int EndsPage { get; set; }
        ///<summary>
        ///The sections within this song. e.g. 6 verse and 1 chorus
        ///</summary>
        public List<SongSection> SongSections { get; set; }
    }
}
