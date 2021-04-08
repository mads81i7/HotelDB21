using System;
using System.Collections.Generic;
using System.Text;
using HotelDBConsole21.Models;

namespace HotelDBConsole21.Interfaces
{
    public interface IGuestService
    {
        List<Guest> GetAllGuests();

        Guest GetGuestFromId(int guestNo);

        bool CreateGuest(Guest guest);

        bool UpdateGuest(Guest guest, int guestNo);

        Guest DeleteGuest(int guestNo);

        List<Guest> GetGuestsByName(string name);
    }
}
