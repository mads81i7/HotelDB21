using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HotelDBConsole21.Interfaces;
using HotelDBConsole21.Models;

namespace HotelDBConsole21.Services
{
    public class RoomServiceAsync: Connection, IRoomServiceAsync
    {
        #region sql strings
        private string queryAllRooms = "Select * from Room;";
        private string queryAllRoomsInHotel = "Select * from Room where Hotel_No = @ID;";
        private string queryGetRoom = "Select * from Room where Room_No = @ID_room and Hotel_No = @ID_hotel;";
        private string insertRoom = "insert into Room Values(@ID_room, @ID_hotel, @Type, @Price);";
        private string updateRoom = "Update Room set Room_No = @ID_room, Hotel_No = @ID_hotel, Types = @Type, Price = @Price where Room_No = @ID_room2 and Hotel_No = @ID_hotel2;";
        private string deleteRoom = "Delete from Room where Room_No = @ID_room and Hotel_No = @ID_hotel";
        #endregion

        public Task<List<Room>> GetAllRoomsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Room>> GetAllRoomsInHotelAsync(int hotelNr)
        {
            throw new NotImplementedException();
        }

        public Task<Room> GetRoomFromIdAsync(int roomNr, int hotelNr)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateRoomAsync(int hotelNr, Room room)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateRoomAsync(Room room, int roomNr, int hotelNr)
        {
            throw new NotImplementedException();
        }

        public Task<Room> DeleteRoomAsync(int roomNr, int hotelNr)
        {
            throw new NotImplementedException();
        }
    }
}
