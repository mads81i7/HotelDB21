using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HotelDBConsole21.Interfaces;
using HotelDBConsole21.Models;
using Microsoft.Data.SqlClient;

namespace HotelDBConsole21.Services
{
    public class HotelServiceAsync: Connection, IHotelServiceAsync
    {
        #region MyRegion
        private String queryString = "select * from Hotel";
        private String queryStringFromID = "select * from Hotel where Hotel_No = @ID";
        private String insertSql = "insert into Hotel Values (@ID, @Navn, @Adresse)";
        private String deleteSql = "delete from Hotel where Hotel_No = @ID";
        private String updateSql = "update Hotel " +
                                   "set Hotel_No= @HotelID, Name=@Navn, Address=@Adresse " +
                                   "where Hotel_No = @ID";
        private String queryStringFromName = "select * from Hotel where Name = @Navn";
        #endregion
        public async Task<List<Hotel>> GetAllHotelAsync()
        {
            List<Hotel> hoteller = new List<Hotel>();

            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync(); //Husk await
                        //Thread.Sleep(1000); // bare for at kunne se forskel
                        SqlDataReader reader = await command.ExecuteReaderAsync();
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

        public async Task<Hotel> GetHotelFromIdAsync(int hotelNr)
        {
            Hotel hotel = new Hotel();
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(queryStringFromID, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@ID", hotelNr);

                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
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

        public async Task<bool> CreateHotelAsync(Hotel hotel)
        {
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(insertSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@ID", hotel.HotelNr);
                        command.Parameters.AddWithValue("@Navn", hotel.Navn);
                        command.Parameters.AddWithValue("@Adresse", hotel.Adresse);

                        await command.Connection.OpenAsync();
                        Thread.Sleep(1000);

                        int noOfRows = await command.ExecuteNonQueryAsync();
                        if (noOfRows == 1)
                        {
                            return true;
                        }

                        return false;
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("FEJL - Database fejl" + sqlEx.Message);
                        return false;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("FEJL - Generel fejl" + ex.Message);
                        return false;
                    }
                }
            }
        }

        public async Task<bool> UpdateHotelAsync(Hotel hotel, int hotelNr)
        {
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(updateSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@ID", hotel.HotelNr);
                        command.Parameters.AddWithValue("@Navn", hotel.Navn);
                        command.Parameters.AddWithValue("@Adresse", hotel.Adresse);
                        command.Parameters.AddWithValue("@ID_2", hotelNr);

                        await command.Connection.OpenAsync();

                        int noOfRows = await command.ExecuteNonQueryAsync();
                        if (noOfRows == 1)
                        {
                            return true;
                        }

                        return false;
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("FEJL - Database fejl" + sqlEx.Message);
                        return false;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("FEJL - Generel fejl" + ex.Message);
                        return false;
                    }

                }
            }
        }

        public async Task<Hotel> DeleteHotelAsync(int hotelNr)
        {
            Hotel hotel = await GetHotelFromIdAsync(hotelNr);
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(deleteSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@ID", hotelNr);
                        await command.Connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
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

        public async Task<List<Hotel>> GetHotelsByNameAsync(string name)
        {
            List<Hotel> hoteller = new List<Hotel>();

            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(queryStringFromName, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@Navn", name);

                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
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
                }
            }
            return hoteller;
        }
    }
}
