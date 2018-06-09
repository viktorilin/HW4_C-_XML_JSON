using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task_XML_JSON;

namespace HotelTest
{
    [TestClass]
    public class HotelTest
    {
        [TestMethod]
        public void HotelToXmlConverterTest()
        {
            var hotelConverter = new HotelConverter();
            var hotel = new Hotel("name", 1, new DateTime(1998, 12, 1), 4, 4.3);
            var result = hotelConverter.HotelToXml(hotel);
            var s = @"<Name>name</Name>
  <HotelId>1</HotelId>
  <FoundedDate>1998-12-01T00:00:00</FoundedDate>
  <TouristCapacity>4</TouristCapacity>
  <Rating>4.3</Rating>
</Hotel>";
            Assert.IsTrue(result.Contains(s));
        }

        [TestMethod]
        public void HotelToJsonTest()
        {
            var hotelConverter = new HotelConverter();
            var hotel = new Hotel("name", 1, new DateTime(1998, 12, 1), 4, 4.3);
            var result = hotelConverter.HotelToJson(hotel);
            var expected =
                "{\"_name\":\"name\",\"_hotelId\":1,\"_foundedDate\":\"1998-12-01T00:00:00\",\"_touristCapacity\":4,\"_rating\":4.3}";
            Assert.AreEqual(expected,result);
        }

        [TestMethod]
        public void HotelFromCsvTest()
        {
            var hotelConverter = new HotelConverter();
            var resultHotel = hotelConverter.HotelFromCsv("Edelweiss; 11; 5/25/1988; 180;4.3;");
            var expectedHotel = new Hotel("Edelweiss", 11, new DateTime(1988, 5, 25), 180, 4.3);
            Assert.AreEqual(resultHotel,expectedHotel);
        }
    }
}