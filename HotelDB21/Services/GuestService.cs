using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using HotelDBConsole21.Interfaces;
using HotelDBConsole21.Models;
using Microsoft.Data.SqlClient;

namespace HotelDBConsole21.Services
{
    public class GuestService: Connection, IGuestService
    {
        #region sql strings

        private string queryGetAllGuests = "select * from Guest;";
        private string queryGetGuest = "select * from Guest where Guest_No = @ID;";
        private string queryGetAllGuestsByName = "select * from Guest where Name = @Navn;";
        private string insertGuest = "insert into Guest Values(@ID, @Navn, @Adresse);";
        private string updateGuest = "Update Guest set Guest_No = @ID, Name = @Navn, Address = @Adresse where Guest_No = @ID_2;";
        private string deleteGuest = "Delete from Guest where Guest_No = @ID;";
        #endregion

        public List<Guest> GetAllGuests()
        {
            List<Guest> guests = new List<Guest>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryGetAllGuests, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int guestNr = reader.GetInt32(0);
                    String guestName = reader.GetString(1);
                    String guestAdr = reader.GetString(2);
                    Guest guest = new Guest(guestNr, guestName, guestAdr);
                    guests.Add(guest);
                }
            }

            return guests;
        }

        public Guest GetGuestFromId(int guestNo)
        {
            Guest guest = new Guest();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryGetGuest, connection);
                command.Parameters.AddWithValue("@ID", guestNo);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    String Navn = reader.GetString(1);
                    String Adr = reader.GetString(2);
                    guest.GuestNo = guestNo;
                    guest.Name = Navn;
                    guest.Address = Adr;
                }
            }

            return guest;
        }
        public List<Guest> GetGuestsByName(string name)
        {
            List<Guest> guests = new List<Guest>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryGetAllGuestsByName, connection);

                command.Parameters.AddWithValue("@Navn", name);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int guestNo = reader.GetInt32(0);
                    String guestName = reader.GetString(1);
                    String guestAdr = reader.GetString(2);
                    Guest guest = new Guest(guestNo, guestName, guestAdr);
                    guests.Add(guest);
                }
            }
            return guests;
        }

        public bool CreateGuest(Guest guest)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(insertGuest, connection);
                command.Parameters.AddWithValue("@ID", guest.GuestNo);
                command.Parameters.AddWithValue("@Navn", guest.Name);
                command.Parameters.AddWithValue("@Adresse", guest.Address);

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

        public bool UpdateGuest(Guest guest, int guestNo)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(updateGuest, connection);
                command.Parameters.AddWithValue("@ID", guest.GuestNo);
                command.Parameters.AddWithValue("@Navn", guest.Name);
                command.Parameters.AddWithValue("@Adresse", guest.Address);
                command.Parameters.AddWithValue("@ID_2", guestNo);

                command.Connection.Open();

                int noOfRows = command.ExecuteNonQuery();
                if (noOfRows == 1)
                {
                    return true;
                }

                return false;
            }
        }

        public Guest DeleteGuest(int guestNo)
        {
            Guest guest = GetGuestFromId(guestNo);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(deleteGuest, connection);
                command.Parameters.AddWithValue("@ID", guestNo);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }

            return guest;
        }
    }
}
