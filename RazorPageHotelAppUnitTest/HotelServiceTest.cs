using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RazorPageHotelApp.Exceptions;
using RazorPageHotelApp.Models;
using RazorPageHotelApp.Services;

namespace RazorPageHotelAppUnitTest
{
    [TestClass]
    public class HotelServiceTest
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HotelDbtest2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        [TestMethod]
        public void CreateHotelAutoIdTest()
        {
            HotelService hotelService = new HotelService(connectionString);
            RoomService roomService = new RoomService(connectionString);

            int lastHotelNoBeforeAdd = hotelService.GetAllHotelAsync().Result[hotelService.GetAllHotelAsync().Result.Count - 1].HotelNr;

            Hotel hotel2 = new Hotel(0, "HotelTest", "TestTown");

            bool ok = hotelService.CreateHotelAsync(hotel2).Result;

            Hotel hotelWithNewId = hotelService.GetHotelsByNameAsync("HotelTest").Result[0];
            List<Room> roomsInHotel = roomService.GetAllRoomsInHotelAsync(hotelWithNewId.HotelNr).Result;

            int lastHotelNoAfterAdd = hotelService.GetAllHotelAsync().Result[hotelService.GetAllHotelAsync().Result.Count - 1].HotelNr;

            Hotel deletedHotel = hotelService.DeleteHotelAsync(hotelWithNewId.HotelNr, roomsInHotel).Result;


            Assert.AreEqual(lastHotelNoBeforeAdd + 1, lastHotelNoAfterAdd);
        }
    }
}
