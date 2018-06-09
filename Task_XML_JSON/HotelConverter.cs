using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Task_XML_JSON
{
    public class HotelConverter
    {
        public Hotel HotelFromCsv(string csvLine)
        {
            string[] values = csvLine.Split(';');

            Hotel hotel = new Hotel();

            hotel.Name = Convert.ToString(values[0].TrimStart());
            hotel.HotelId = Convert.ToInt32(values[1].TrimStart());
            hotel.FoundedDate = DateTime.ParseExact(values[2].TrimStart(), "M/d/yyyy", CultureInfo.InvariantCulture);
            hotel.TouristCapacity = Convert.ToInt32(values[3].TrimStart());
            if (values[4].Equals(""))
                hotel.Rating = 0;
            else
                hotel.Rating = Convert.ToDouble(values[4].Replace(".", ","));

            return hotel;
        }

        public string HotelToJson(Hotel hotel) // method for unit testing example
        {
            var data = JsonConvert.SerializeObject(hotel);
            return data;
        }

        public string HotelToXml(Hotel hotel) // method for unit testing example
        {
            var stringWriter = new StringWriter();
            var serializer = new XmlSerializer(hotel.GetType());
            serializer.Serialize(stringWriter, hotel);
            return stringWriter.ToString();
        }

        public string HotelToJson(Hotel[] hotels) // method for unit testing example
        {
            var data = JsonConvert.SerializeObject(hotels);
            return data;
        }

        public string HotelToXml(Hotel[] hotels) // method for unit testing example
        {
            var stringWriter = new StringWriter();
            var serializer = new XmlSerializer(hotels.GetType());
            serializer.Serialize(stringWriter, hotels);
            return stringWriter.ToString();
        }

        public void HotelToJson(Hotel hotel, string path) 
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(HotelToJson(hotel));
            }
        }

        public void HotelToXml(Hotel hotel, string path)
        {
            XmlSerializer formatter = new XmlSerializer(hotel.GetType());

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, hotel);
                Console.WriteLine("File is converted to XML. Verify at " + path);
            }
        }

        public void HotelToJson(Hotel[] hotels, string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(HotelToJson(hotels));
            }
        }

        public void HotelToXml(Hotel[] hotels, string path)
        {
            XmlSerializer formatter = new XmlSerializer(hotels.GetType());

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, hotels);
            }
        }
    }
}