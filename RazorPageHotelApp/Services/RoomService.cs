using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RazorPageHotelApp.Interfaces;
using RazorPageHotelApp.Models;

namespace RazorPageHotelApp.Services
{
    public class RoomService: Connection, IRoomService
    {
        #region sql strings
        private string queryAllRooms = "Select * from Room;";
        private string queryAllRoomsInHotel = "Select * from Room where Hotel_No = @ID;";

        private string queryAllRoomsInHotelWithType = "SELECT * FROM Room WHERE Types = @Type AND Hotel_No = @ID;";
        private string queryAllRoomsInHotelWithPrice = "SELECT * from Room WHERE Price <= @MaxPrice AND Hotel_No = @ID;";
        private string queryAllRoomsInHotelSorted = "SELECT * FROM Room WHERE Types = @Type AND Hotel_No = @ID ORDER BY ";
        private string queryAllRoomsInHotelSortedNoType = "SELECT * FROM Room WHERE Hotel_No = @ID ORDER BY ";

        private string queryTest = "SELECT * FROM Room WHERE Hotel_No = @ID ORDER BY ";

        private string queryGetRoom = "Select * from Room where Room_No = @ID_room and Hotel_No = @ID_hotel;";
        private string insertRoom = "insert into Room Values(@ID_room, @ID_hotel, @Type, @Price);";
        private string updateRoom = "Update Room set Room_No = @ID_room, Hotel_No = @ID_hotel, Types = @Type, Price = @Price where Room_No = @ID_room2 and Hotel_No = @ID_hotel2;";
        private string deleteRoom = "Delete from Room where Room_No = @ID_room and Hotel_No = @ID_hotel";
        #endregion
        public RoomService(IConfiguration configuration) : base(configuration)
        {
        }

        public RoomService(string connectionString):base(connectionString)
        {
        }


        public async Task<List<Room>> GetAllRoomsAsync()
        {
            List<Room> rooms = new List<Room>();

            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(queryAllRooms, connection))
                {
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int roomNr = reader.GetInt32(0);
                        int hotelNr = reader.GetInt32(1);
                        String type = reader.GetString(2);
                        double price = reader.GetDouble(3);
                        Room room = new Room(roomNr, type[0], price, hotelNr);
                        rooms.Add(room);
                    }
                }
            }
            return rooms;
        }

        public async Task<List<Room>> GetAllRoomsInHotelAsync(int hotelNr)
        {
            List<Room> rooms = new List<Room>();

            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(queryAllRoomsInHotel, connection))
                {
                    command.Parameters.AddWithValue("@ID", hotelNr);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int roomNr = reader.GetInt32(0);
                        int hotelNo = reader.GetInt32(1);
                        String type = reader.GetString(2);
                        double price = reader.GetDouble(3);
                        Room room = new Room(roomNr, type[0], price, hotelNr);
                        rooms.Add(room);
                    }
                }
            }
            return rooms;
        }

        public async Task<Room> GetRoomFromIdAsync(int roomNr, int hotelNr)
        {
            Room room = new Room();

            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(queryGetRoom, connection))
                {
                    command.Parameters.AddWithValue("@ID_room", roomNr);
                    command.Parameters.AddWithValue("@ID_hotel", hotelNr);

                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        room.RoomNr = roomNr;
                        room.HotelNr = hotelNr;
                        room.Types = reader.GetString(2)[0];
                        room.Pris = reader.GetDouble(3);
                    }
                }
            }
            return room;
        }

        public async Task<bool> CreateRoomAsync(int hotelNr, Room room)
        {
            List<Room> roomsInHotel = await GetAllRoomsInHotelAsync(hotelNr);
            List<int> IdList = new List<int>();
            foreach (Room r in roomsInHotel)
            {
                IdList.Add(r.RoomNr);
            }

            int id = 1;
            if (IdList.Count != 0)
            {
                id = IdList.Max() + 1;
            }
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(insertRoom, connection))
                {
                    command.Parameters.AddWithValue("@ID_room", id);
                    command.Parameters.AddWithValue("@Type", room.Types);
                    command.Parameters.AddWithValue("@Price", room.Pris);
                    command.Parameters.AddWithValue("@ID_hotel", room.HotelNr);

                    await command.Connection.OpenAsync();

                    int noOfRows = await command.ExecuteNonQueryAsync();
                    if (noOfRows == 1)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        public async Task<bool> UpdateRoomAsync(Room room, int roomNr, int hotelNr)
        {
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(updateRoom, connection))
                {
                    command.Parameters.AddWithValue("@ID_room", room.RoomNr);
                    command.Parameters.AddWithValue("@ID_hotel", room.HotelNr);
                    command.Parameters.AddWithValue("@Type", room.Types);
                    command.Parameters.AddWithValue("@Price", room.Pris);
                    command.Parameters.AddWithValue("ID_room2", roomNr);
                    command.Parameters.AddWithValue("@ID_hotel2", hotelNr);

                    await command.Connection.OpenAsync();

                    int noOfRows = await command.ExecuteNonQueryAsync();
                    if (noOfRows == 1)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        public async Task<Room> DeleteRoomAsync(int roomNr, int hotelNr)
        {
            Room room = await GetRoomFromIdAsync(roomNr, hotelNr);
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(deleteRoom, connection))
                {
                    command.Parameters.AddWithValue("@ID_room", roomNr);
                    command.Parameters.AddWithValue("@ID_hotel", hotelNr);
                    await command.Connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
            return room;
        }
        public async Task<List<Room>> GetAllRoomsInHotelWithTypeAsync(int hotelNr, char roomType)
        {
            List<Room> rooms = new List<Room>();

            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(queryAllRoomsInHotelWithType, connection))
                {
                    command.Parameters.AddWithValue("@ID", hotelNr);
                    command.Parameters.AddWithValue("@Type", roomType);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int roomNr = reader.GetInt32(0);
                        int hotelNo = reader.GetInt32(1);
                        String type = reader.GetString(2);
                        double price = reader.GetDouble(3);
                        Room room = new Room(roomNr, type[0], price, hotelNr);
                        rooms.Add(room);
                    }
                }
            }
            return rooms;
        }

        public async Task<List<Room>> GetAllRoomsInHotelSortedWithTypeAsync(int hotelNr, char roomType, string sortCriteria, string direction)
        {
            List<Room> rooms = new List<Room>();

            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(queryAllRoomsInHotelSorted + sortCriteria + direction, connection))
                {
                    command.Parameters.AddWithValue("@ID", hotelNr);
                    command.Parameters.AddWithValue("@Type", roomType);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int roomNr = reader.GetInt32(0);
                        int hotelNo = reader.GetInt32(1);
                        String type = reader.GetString(2);
                        double price = reader.GetDouble(3);
                        Room room = new Room(roomNr, type[0], price, hotelNr);
                        rooms.Add(room);
                    }
                }
            }
            return rooms;
        }
        public async Task<List<Room>> GetAllRoomsInHotelSortedAsync(int hotelNr, string sortCriteria, string direction)
        {
            List<Room> rooms = new List<Room>();

            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(queryAllRoomsInHotelSortedNoType + sortCriteria + direction, connection))
                {
                    command.Parameters.AddWithValue("@ID", hotelNr);
                    //command.Parameters.AddWithValue("@Criteria", sortCriteria);
                    //command.Parameters.AddWithValue("@Direction", direction);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int roomNr = reader.GetInt32(0);
                        int hotelNo = reader.GetInt32(1);
                        String type = reader.GetString(2);
                        double price = reader.GetDouble(3);
                        Room room = new Room(roomNr, type[0], price, hotelNr);
                        rooms.Add(room);
                    }
                }
            }
            return rooms;
        }

    }
}
