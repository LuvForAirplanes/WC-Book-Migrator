using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace WCBookMigrator
{
    class Program
    {
        static void Main(string[] args)
        {
            //var text = File.ReadAllText();
            //Console.WriteLine(text.Take(1000).ToArray());

            XmlSerializer serializer = new XmlSerializer(typeof(object));
            object resultingMessage = (object)serializer.Deserialize(new XmlTextReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "raw.txt")), );
        }
    }
}
