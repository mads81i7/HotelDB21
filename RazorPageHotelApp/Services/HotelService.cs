using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RazorPageHotelApp.Exceptions;
using RazorPageHotelApp.Interfaces;
using RazorPageHotelApp.Models;

namespace RazorPageHotelApp.Services
{
    public class HotelService: Connection, IHotelService
    {
        #region MyRegion
        private String queryString = "select * from Hotel";
        private String queryStringFromID = "select * from Hotel where Hotel_No = @ID";
        private String queryStringFromName = "select * from Hotel where Name = @Navn";
        private String insertSql = "insert into Hotel Values (@ID, @Navn, @Adresse)";
        private String deleteSql = "delete from Hotel where Hotel_No = @ID";
        private String updateSql = "update Hotel " +
                                   "set Hotel_No= @ID, Name=@Navn, Address=@Adresse " +
                                   "where Hotel_No = @ID_2";
        #endregion


        public HotelService(IConfiguration configuration) : base(configuration)
        {
        }

        public HotelService(string connectionString) : base(connectionString)
        {
        }
        public async Task<List<Hotel>> GetAllHotelAsync()
        {
            List<Hotel> hoteller = new List<Hotel>();
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();

                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
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
                        while (await reader.ReadAsync())
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
            List<Hotel> hotels = await GetAllHotelAsync();
            List<int> IdList = new List<int>();
            foreach (Hotel h in hotels)
            {
                IdList.Add(h.HotelNr);
            }

            int id = 1;
            if (IdList.Count != 0)
            {
                id = IdList.Max() + 1;
            }
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(insertSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@ID", id);
                        command.Parameters.AddWithValue("@Navn", hotel.Navn);
                        command.Parameters.AddWithValue("@Adresse", hotel.Adresse);

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

        public async Task<Hotel> DeleteHotelAsync(int hotelNr, List<Room> roomsInHotel)
        {
            Hotel hotel = await GetHotelFromIdAsync(hotelNr);
            if (roomsInHotel.Count != 0)
            {
                throw new HotelContainsRoomsException($"The Hotel: {hotel.Navn} contains rooms and can thus not be deleted");
            }
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
                        while (await reader.ReadAsync())
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
