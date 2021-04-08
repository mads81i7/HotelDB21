using System;
using System.Collections.Generic;
using System.Text;
using HotelDBConsole21.Interfaces;
using HotelDBConsole21.Models;
using Microsoft.Data.SqlClient;

namespace HotelDBConsole21.Services
{
    public class HotelService : Connection, IHotelService
    {
        private string queryString = "select * from Hotel";
        private String queryStringFromID = "select * from Hotel where Hotel_No = @ID";
        private string insertSql = "insert into Hotel Values(@ID, @Navn, @Adresse)";

        private string deleteSql = "Delete from Hotel where Hotel_No = @ID";

        private string updateSql = "Update Hotel set Hotel_No = @ID, Name = @Navn, Address = @Adresse where Hotel_No = @ID_2";
        private String queryStringFromName = "select * from Hotel where Name = @Navn";
        // lav selv sql strengene færdige og lav gerne yderligere sqlstrings


        public List<Hotel> GetAllHotel()
        {
            List<Hotel> hoteller = new List<Hotel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    try
                    {
                        command.Connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int hotelNr = reader.GetInt32(0);
                            String hotelNavn = reader.GetString(1);
                            String hotelAdr = reader.GetString(2);
                            Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                            hoteller.Add(hotel);
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("FEJL - Database fejl" + sqlEx.Message);
                        return null;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("FEJL - Generel fejl" + ex.Message);
                        return null;
                    }
                    finally
                    {
                        //Her kommer man ind lige meget hvad. både når man kommer ind i catch og når man ikke gør
                    }
                    
                }
            }
            return hoteller;
        }

        public Hotel GetHotelFromId(int hotelNr)
        {
            Hotel hotel = new Hotel();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryStringFromID, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@ID", hotelNr);

                        command.Connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            String hotelNavn = reader.GetString(1);
                            String hotelAdr = reader.GetString(2);
                            hotel.HotelNr = hotelNr;
                            hotel.Navn = hotelNavn;
                            hotel.Adresse = hotelAdr;
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("FEJL - Database fejl" + sqlEx.Message);
                        return null;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("FEJL - Generel fejl" + ex.Message);
                        return null;
                    }
                }
            }
            return hotel;
        }

        public bool CreateHotel(Hotel hotel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertSql, connection))
                {
                    command.Parameters.AddWithValue("@ID", hotel.HotelNr);
                    command.Parameters.AddWithValue("@Navn", hotel.Navn);
                    command.Parameters.AddWithValue("@Adresse", hotel.Adresse);

                    command.Connection.Open();

                    int noOfRows =
                        command.ExecuteNonQuery(); //bruges ved update, delete og insert eller når man ikke skal læse fra DB
                    if (noOfRows == 1)
                    {
                        return true;
                    }

                    return false;
                }
            }
        }

        public bool UpdateHotel(Hotel hotel, int hotelNr)
        {
            //DeleteHotel(hotelNr);
            //return CreateHotel(hotel);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(updateSql, connection))
                {
                    command.Parameters.AddWithValue("@ID", hotel.HotelNr);
                    command.Parameters.AddWithValue("@Navn", hotel.Navn);
                    command.Parameters.AddWithValue("@Adresse", hotel.Adresse);
                    command.Parameters.AddWithValue("@ID_2", hotelNr);

                    command.Connection.Open();

                    int noOfRows = command.ExecuteNonQuery();
                    if (noOfRows == 1)
                    {
                        return true;
                    }

                    return false;
                }
            }
        }

        public Hotel DeleteHotel(int hotelNr)
        {
            Hotel hotel = GetHotelFromId(hotelNr);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(deleteSql, connection))
                {
                    command.Parameters.AddWithValue("@ID", hotelNr);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return hotel;
        }

        public List<Hotel> GetHotelsByName(string name)
        {
            List<Hotel> hoteller = new List<Hotel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryStringFromName, connection))
                {
                    command.Parameters.AddWithValue("@Navn", name);

                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int hotelNr = reader.GetInt32(0);
                        String hotelNavn = reader.GetString(1);
                        String hotelAdr = reader.GetString(2);
                        Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                        hoteller.Add(hotel);
                    }
                }
                
            }
            return hoteller;
        }
    }
}
