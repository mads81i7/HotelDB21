using System;
using System.Collections.Generic;
using System.Text;
using HotelDBConsole21.Interfaces;
using HotelDBConsole21.Models;
using Microsoft.Data.SqlClient;

namespace HotelDBConsole21.Services
{
    public class BookingService: Connection, IBookingService
    {
        #region sql strings
        private string queryGetAllBookings = "select * from Booking;";
        private string queryGetBooking = "select * from Booking where Booking_No = @ID;";

        private string insertBooking = "insert into Booking Values(@ID_booking, @ID_hotel, @ID_guest, @DateFrom, @DateTo, @ID_room);";
        private string updateBooking = "Update Booking set Booking_id = @ID, Hotel_No = @ID_hotel, Guest_No = @ID_guest, Date_From = @DateFrom, Date_To = @DateTo, Room_No = ID_room where Booking_id = @ID_2;";
        private string deleteBooking = "Delete from Booking where Booking_id = @ID;";
        #endregion

        public List<Booking> GetAllBookings()
        {
            List<Booking> bookings = new List<Booking>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryGetAllBookings, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int bookingNo = reader.GetInt32(0);
                    int HotelNo = reader.GetInt32(1);
                    int GuestNo = reader.GetInt32(2);
                    DateTime DateFrom = reader.GetDateTime(3);
                    DateTime DateTo = reader.GetDateTime(4);
                    int RoomNo = reader.GetInt32(5);
                    Booking booking = new Booking(bookingNo, HotelNo, GuestNo, DateFrom, DateTo, RoomNo);
                    bookings.Add(booking);
                }
            }
            return bookings;
        }

        public Booking GetBookingFromId(int bookingNo)
        {
            throw new NotImplementedException();
        }

        public bool CreateBooking(Booking booking)
        {
            throw new NotImplementedException();
        }

        public bool UpdateBooking(Booking booking, int bookingNo)
        {
            throw new NotImplementedException();
        }

        public Booking DeleteBooking(int bookingNo)
        {
            throw new NotImplementedException();
        }

        public List<Booking> GetBookingsFromDate()
        {
            throw new NotImplementedException();
        }

        public Hotel GetHotelFromBooking()
        {
            throw new NotImplementedException();
        }

        public Guest GetGuestFromBooking()
        {
            throw new NotImplementedException();
        }

        public Room GetRoomFromBooking()
        {
            throw new NotImplementedException();
        }
    }
}
