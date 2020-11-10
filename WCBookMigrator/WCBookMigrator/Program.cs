using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using WCBookMigrator.Models;
using Humanizer;

namespace WCBookMigrator
{
    public class Program
    {
        static void Main(string[] args)
        {
            var songs = new List<Song>();
            var tunes = new List<Tune>();
            var meters = new List<Meter>();
            var parts = new List<BookPart>();
            var sections = new List<BookSection>();

            XmlSerializer serializer = new XmlSerializer(typeof(object), new XmlRootAttribute("car"));
            var nodes = (XmlNode[])serializer.Deserialize(new XmlTextReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "raw.xml")));
            
            var lastSectionId = "";
            var lastPartId = "";
            var lastSong = new Song
            {
                Number = 0
            };
            
            for (int i = 0; i < nodes.Length; i++)
            {
                if(nodes[i].Name == "h1")
                {
                    //we're at a header
                    var bookPart = new BookPart{
                        Id = nodes[i].InnerText.Underscore(),
                        Name = nodes[i].InnerText.Humanize(LetterCasing.Title)
                    };

                    parts.Add(bookPart);

                    lastPartId = bookPart.Id;
                }
                else if (nodes[i].Name == "h2" && nodes[i].NextSibling.Name == "h2")
                {
                    //were at a section title
                    var bookSection = new BookSection
                    {
                        Id = nodes[i].Name.Underscore(),
                        BookPartId = lastPartId,
                        Name = nodes[i].Name.Humanize(LetterCasing.Title)
                    };

                    sections.Add(bookSection);

                    lastSectionId = bookSection.Id;
                }
                else if (nodes[i].Name == "h2" && nodes[i].NextSibling.Name == "p")
                {
                    var song = new Song
                    {
                        Number = lastSong.Number + 1,
                        Id = (lastSong.Number + 1).ToString(),
                        BookSectionId = lastSectionId
                    };
                }
            }
        }
    }
}
