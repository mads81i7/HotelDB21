using System;
using System.Collections.Generic;
using System.Text;

namespace HotelDBConsole21.Models
{
    public class Booking
    {
        private int _bookingID;

        public int BookingID
        {
            get { return _bookingID; }
            set { _bookingID = value; }
        }
        private int _hotelNo;

        public int HotelNo
        {
            get { return _hotelNo; }
            set { _hotelNo = value; }
        }
        private int _guestNo;

        public int GuestNo
        {
            get { return _guestNo; }
            set { _guestNo = value; }
        }
        private DateTime _dateFrom;

        public DateTime DateFrom   
        {
            get { return _dateFrom; }
            set { _dateFrom = value; }
        }
        private DateTime _dateTo;

        public DateTime DateTo
        {
            get { return _dateTo; }
            set { _dateTo = value; }
        }
        private int _roomNo;

        public int RoomNo
        {
            get { return _roomNo; }
            set { _roomNo = value; }
        }

        public Booking()
        {
        }

        public Booking(int bookingId, int hotelNo, int guestNo, DateTime dateFrom, DateTime dateTo, int roomNo)
        {
            _bookingID = bookingId;
            _hotelNo = hotelNo;
            _guestNo = guestNo;
            _dateFrom = dateFrom;
            _dateTo = dateTo;
            _roomNo = roomNo;
        }

        public override string ToString()
        {
            return $"Booking ID: {_bookingID} | Hotel No: {_hotelNo} | Guest No: {_guestNo} | Room No: {_roomNo} | Date From and To: {_dateFrom} - {_dateTo}";
        }
    }
}
