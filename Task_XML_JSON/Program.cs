﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Xml.Linq;
using System.Xml.Serialization;


namespace Task_XML_JSON
{
    class Program
    {
        static void Main(string[] args)
        {
            string outputPathXml = ConfigurationManager.AppSettings["OutputPathXML"];
            string outputPathJson = ConfigurationManager.AppSettings["OutputPathJSON"];
            string inputPath = ConfigurationManager.AppSettings["InputPath"];

            string csvString = @"Edelweiss; 11; 5/25/1988; 180;4.3; 
Bristol; 12; 4/3/1965; 140;4.1;
Hilton; 1; 10/02/1863; 321;2,0; 
Balaton; 50; 5/1/2000; 110;; 
Empire; 13; 7/25/2011; 240;5,0;";

            File.WriteAllText(inputPath, csvString);

            List<Hotel> hotels = File.ReadAllLines(inputPath)
                .Select(v => Hotel.FromCsv(v))
                .ToList();
            hotels.Sort(((hotel, hotel1) => hotel1.Rating.CompareTo(hotel.Rating)));
            string format = "";
            while (!format.Equals("JSON") && !format.Equals("XML"))
            {
                Console.WriteLine("Input converter XML or JSON");

                format = Console.ReadLine();
                if (format.Equals("JSON"))
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Hotel[]));

                    using (FileStream fs = new FileStream(outputPathJson, FileMode.OpenOrCreate))
                    {
                        ser.WriteObject(fs, hotels.ToArray());
                    }

                    Console.WriteLine("File is converted to JSON.Verify at " + outputPathJson);
                    Console.WriteLine(File.ReadAllText(outputPathJson));
                    
                }
                else if (format.Equals("XML"))
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(Hotel[]));

                    using (FileStream fs = new FileStream(outputPathXml, FileMode.OpenOrCreate))
                    {
                        formatter.Serialize(fs, hotels.ToArray());

                        Console.WriteLine("File is converted to XML. Verify at " + outputPathXml);
                    }

                    Console.WriteLine(File.ReadAllText(outputPathXml));
                }
            }

            Console.Read();
        }
    }
}