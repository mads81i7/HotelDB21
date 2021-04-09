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

        [TestMethod]
        public void TestAddHotel()
        {
            //Arrange
            HotelService hotelService = new HotelService(connectionString);
            RoomService roomService = new RoomService(connectionString);
            List<Hotel> hotels = hotelService.GetAllHotelAsync().Result;
            

            //Act
            int numbersOfHotelsBefore = hotels.Count;
            Hotel newHotel = new Hotel(1001, "TestHotel10293847", "Testvej");
            bool ok = hotelService.CreateHotelAsync(newHotel).Result;
            hotels = hotelService.GetAllHotelAsync().Result;

            Hotel newHotelWithNewId = hotelService.GetHotelsByNameAsync("TestHotel10293847").Result[0];

            int numbersOfHotelsAfter = hotels.Count;
            List<Room> roomsInHotel = roomService.GetAllRoomsInHotelAsync(newHotel.HotelNr).Result;
            Hotel h = hotelService.DeleteHotelAsync(newHotelWithNewId.HotelNr, roomsInHotel).Result;

            //Assert
            Assert.AreEqual(numbersOfHotelsBefore + 1, numbersOfHotelsAfter);
            Assert.IsTrue(ok);
            Assert.AreEqual(h.HotelNr, newHotelWithNewId.HotelNr);
        }

    }

}

