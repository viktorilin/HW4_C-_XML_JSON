using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Task_XML_JSON
{
    [Serializable]
    [DataContract]
    public class Hotel
    {
        [DataMember]
        private string _name;
        [DataMember]
        private int _hotelId;
        [DataMember]
        private DateTime _foundedDate;
        [DataMember]
        private int _touristCapacity;
        [DataMember]
        private double _rating;

        public Hotel()
        {
        }

        public Hotel(string name, int hotelId, DateTime foundedDate, int touristCapacity, double rating)
        {
            this._name = name;
            this._hotelId = hotelId;
            this._foundedDate = foundedDate;
            this._touristCapacity = touristCapacity;
            this._rating = rating;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public int HotelId
        {
            get => _hotelId;
            set => _hotelId = value;
        }

        public DateTime FoundedDate
        {
            get => _foundedDate;
            set => _foundedDate = value;
        }

        public int TouristCapacity
        {
            get => _touristCapacity;
            set => _touristCapacity = value;
        }

        public double Rating
        {
            get => _rating;
            set => _rating = value;
        }

        public static Hotel FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(';');

            Hotel hotel = new Hotel();

            hotel.Name = Convert.ToString(values[0].TrimStart());
            hotel.HotelId = Convert.ToInt32(values[1].TrimStart());
            hotel._foundedDate = DateTime.ParseExact(values[2].TrimStart(), "M/d/yyyy", CultureInfo.InvariantCulture);
            hotel.TouristCapacity = Convert.ToInt32(values[3].TrimStart());
            if (values[4].Equals(""))
                hotel.Rating = 0;
            else
                hotel.Rating = Convert.ToDouble(values[4].Replace(".", ","));

            return hotel;
        }
    }
}