using System;
using System.Collections.Generic;
using System.Text;
using HotelDBConsole21.Models;

namespace HotelDBConsole21.Interfaces
{
    public interface IBookingService
    {
        List<Booking> GetAllBookings();

        Booking GetBookingFromId(int bookingNo);

        bool CreateBooking(Booking booking);

        bool UpdateBooking(Booking booking, int bookingNo);

        Booking DeleteBooking(int bookingNo);
        List<Booking> GetBookingsFromDate();

        Hotel GetHotelFromBooking();
        Guest GetGuestFromBooking();
        Room GetRoomFromBooking();

    }
}
