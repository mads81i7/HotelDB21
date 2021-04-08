using System;
using System.Collections.Generic;
using System.Text;
using HotelDBConsole21.Interfaces;
using HotelDBConsole21.Models;
using Microsoft.Data.SqlClient;

namespace HotelDBConsole21.Services
{
    

    public class RoomService: Connection, IRoomService
    {
        // lad klassen arve fra interfacet IRoomService og arve fra Connection klassen
        // indsæt sql strings
        private string queryAllRooms = "Select * from Room;";
        private string queryAllRoomsInHotel = "Select * from Room where Hotel_No = @ID;";
        private string queryGetRoom = "Select * from Room where Room_No = @ID_room and Hotel_No = @ID_hotel;";
        private string insertRoom = "insert into Room Values(@ID_room, @ID_hotel, @Type, @Price);";
        private string updateRoom = "Update Room set Room_No = @ID_room, Hotel_No = @ID_hotel, Types = @Type, Price = @Price where Room_No = @ID_room2 and Hotel_No = @ID_hotel2;";
        private string deleteRoom = "Delete from Room where Room_No = @ID_room and Hotel_No = @ID_hotel";
        //Implementer metoderne som der skal ud fra interfacet

        public List<Room> GetAllRooms()
        {
            List<Room> rooms = new List<Room>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryAllRooms, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int roomNr = reader.GetInt32(0);
                    int hotelNr = reader.GetInt32(1);
                    String type = reader.GetString(2);
                    double price = reader.GetDouble(3);
                    Room room = new Room(roomNr, type[0], price, hotelNr);
                    rooms.Add(room);
                }
            }

            return rooms;
        }

        public List<Room> GetAllRoomsInHotel(int hotelNo)
        {
            List<Room> rooms = new List<Room>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryAllRoomsInHotel, connection);
                command.Parameters.AddWithValue("@ID", hotelNo);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int roomNr = reader.GetInt32(0);
                    int hotelNr = reader.GetInt32(1);
                    String type = reader.GetString(2);
                    double price = reader.GetDouble(3);
                    Room room = new Room(roomNr, type[0], price, hotelNr);
                    rooms.Add(room);
                }
            }

            return rooms;
        }

        public Room GetRoomFromId(int roomNr, int hotelNr)
        {
            Room room = new Room();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryGetRoom, connection);
                command.Parameters.AddWithValue("@ID_room", roomNr);
                command.Parameters.AddWithValue("@ID_hotel", hotelNr);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    room.RoomNr = roomNr;
                    room.HotelNr = hotelNr;
                    room.Types = reader.GetString(2)[0];
                    room.Pris = reader.GetDouble(3);
                }
            }

            return room;
        }

        public bool CreateRoom(int hotelNr, Room room)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(insertRoom, connection);
                command.Parameters.AddWithValue("@ID_room", room.RoomNr);
                command.Parameters.AddWithValue("@Type", room.Types);
                command.Parameters.AddWithValue("@Price", room.Pris);
                command.Parameters.AddWithValue("@ID_hotel", room.HotelNr);

                command.Connection.Open();

                int noOfRows =
                    command.ExecuteNonQuery();
                if (noOfRows == 1)
                {
                    return true;
                }

                return false;
            }
        }

        public bool UpdateRoom(Room room, int roomNr, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(updateRoom, connection);
                command.Parameters.AddWithValue("@ID_room", room.RoomNr);
                command.Parameters.AddWithValue("@ID_hotel", room.HotelNr);
                command.Parameters.AddWithValue("@Type", room.Types);
                command.Parameters.AddWithValue("@Price", room.Pris);
                command.Parameters.AddWithValue("ID_room2", roomNr);
                command.Parameters.AddWithValue("@ID_hotel2", hotelNr);

                command.Connection.Open();

                int noOfRows = command.ExecuteNonQuery();
                if (noOfRows == 1)
                {
                    return true;
                }

                return false;
            }
        }

        public Room DeleteRoom(int roomNr, int hotelNr)
        {
            Room room = GetRoomFromId(roomNr, hotelNr);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(deleteRoom, connection);
                command.Parameters.AddWithValue("@ID_room", roomNr);
                command.Parameters.AddWithValue("@ID_hotel", hotelNr);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }

            return room;
        }
    }
}
