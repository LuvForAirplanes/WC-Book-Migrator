using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using WCBookMigrator.Models;
using Humanizer;
using ChoETL;

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
            var songSection = new List<SongSection>();
            var songSectionLine = new List<SongSectionLine>();

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
                    var children = nodes[i].ChildNodes;
                    //were at a section title
                    var bookSection = new BookSection
                    {
                        Id = nodes[i].InnerText.Underscore(),
                        BookPartId = lastPartId,
                        Name = nodes[i].InnerText.Humanize(LetterCasing.Title)
                    };

                    sections.Add(bookSection);

                    lastSectionId = bookSection.Id;
                }
                else if (nodes[i].Name == "h2" && nodes[i].NextSibling.Name == "p")
                {
                    var rawMeterName = nodes[i].ChildNodes[1].InnerText;
                    var meter = meters.FirstOrDefault(m => m.Name == rawMeterName);

                    if(meter == null)
                    {
                        var newMeter = new Meter
                        {
                            Id = rawMeterName,
                            Name = rawMeterName
                        };

                        meters.Add(newMeter);
                        meter = newMeter;
                    }

                    var rawTuneName = nodes[i].ChildNodes[2].InnerText.Replace("[", "").Replace(".", "");
                    var tune = tunes.FirstOrDefault(t => t.Name == rawTuneName);

                    if(tune == null)
                    {
                        var newTune = new Tune
                        {
                            Id = rawTuneName,
                            Name = rawTuneName
                        };

                        tunes.Add(newTune);
                        tune = newTune;
                    }

                    var song = new Song
                    {
                        Number = lastSong.Number + 1,
                        Id = (lastSong.Number + 1).ToString(),
                        BookSectionId = lastSectionId,
                        MeterId = meter.Id,
                        TuneId = tune.Id
                    };

                    songs.Add(song);
                    lastSong = song;
                }
                else if(nodes[i].Name == "p")
                {
                    //song body
                    var body = nodes[i].InnerText.Replace("<span>", "").Replace("</span>", "");
                    var songSections = body.Split("\r\n    \r\n", StringSplitOptions.None).ToList();

                    for (int ii = 0; ii < songSections.Count; ii++)
                    {
                        if(songSections[ii].Count() <= 1)
                            break;

                        var section = new SongSection
                        {
                            Id = ii.ToString(),
                            SongId = lastSong.Id,
                            Order = ii
                        };

                        songSection.Add(section);

                        var currentSongLines = songSections[ii].Split("\n").ToList();

                        //remove spacing
                        currentSongLines.RemoveAll(r => r == "\r");
                        for (int b = 0; b < currentSongLines.Count; b++)
                        {
                            currentSongLines[b] = currentSongLines[b].Replace("    ", "").Replace("\r", "");
                        }
                        
                        for (int iii = 0; iii < currentSongLines.Count; iii++)
                        {
                            songSectionLine.Add(new SongSectionLine
                            {
                                Id = iii.ToString(),
                                Order = iii,
                                SongSectionId = section.Id,
                                Line = currentSongLines[iii]
                            });
                        }
                    }
                }
            }

            using(var writer = new ChoCSVWriter<Song>(@"C:\output\songs.csv")) 
            {
                writer.Write(songs);
            }

            using(var writer = new ChoCSVWriter<Tune>(@"C:\output\tunes.csv")) 
            {
                writer.Write(tunes);
            }

            using(var writer = new ChoCSVWriter<Meter>(@"C:\output\meters.csv")) 
            {
                writer.Write(meters);
            }

            using(var writer = new ChoCSVWriter<BookPart>(@"C:\output\book_parts.csv")) 
            {
                writer.Write(parts);
            }

            using(var writer = new ChoCSVWriter<BookSection>(@"C:\output\book_section.csv")) 
            {
                writer.Write(sections);
            }

            using(var writer = new ChoCSVWriter<SongSection>(@"C:\output\song_sections.csv")) 
            {
                writer.Write(songSection);
            }

            using(var writer = new ChoCSVWriter<SongSectionLine>(@"C:\output\song_section_lines.csv")) 
            {
                writer.Write(songSectionLine);
            }
        }
    }
}
