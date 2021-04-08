using System;
using System.Collections.Generic;
using System.Text;

namespace HotelDBConsole21.Models
{
    public class Guest
    {
        private int _guestNo;

        public int GuestNo
        {
            get { return _guestNo; }
            set { _guestNo = value; }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _address;

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        public Guest()
        {
        }
        public Guest(int guestNo, string name, string address)
        {
            _guestNo = guestNo;
            _name = name;
            _address = address;
        }

        public override string ToString()
        {
            return ($"Guest No: {_guestNo} | Name: {_name} |  Address: {_address}");
        }
    }
}
