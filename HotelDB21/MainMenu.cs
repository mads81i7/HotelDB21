using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HotelDBConsole21.Models;
using HotelDBConsole21.Services;

namespace HotelDBConsole21
{
    public static class MainMenu
    {
        //Lav selv flere menupunkter til at vælge funktioner for Rooms
        public static void showOptions()
        {
            Console.Clear();
            Console.WriteLine("Vælg et menupunkt");
            Console.WriteLine("---------HOTEL---------");
            Console.WriteLine("1) List hoteller");
            Console.WriteLine("1a) List hoteller async");
            Console.WriteLine("2) Opret nyt Hotel");
            Console.WriteLine("2a) Opret nyt Hotel");
            Console.WriteLine("3) Fjern Hotel");
            Console.WriteLine("4) Søg efter hotel udfra hotelnr");
            Console.WriteLine("5) Opdater et hotel");
            Console.WriteLine("6) Søg efter hotel udfra navn");
            Console.WriteLine("\n---------VÆRELSE---------");
            Console.WriteLine("7) List alle værelser");
            Console.WriteLine("8) List alle værelser til et bestemt hotel");
            Console.WriteLine("9) Søg efter værelse udfra VærelsesNr og HotelNr");
            Console.WriteLine("10) Opret nyt værelse");
            Console.WriteLine("11) Opdater et værelse");
            Console.WriteLine("12) Fjern værelse");
            Console.WriteLine("\n---------GÆST---------");
            Console.WriteLine("13) List gæster");
            Console.WriteLine("14) Opret ny Gæst");
            Console.WriteLine("15) Fjern Gæst");
            Console.WriteLine("16) Søg efter Gæst udfra GuestNo");
            Console.WriteLine("17) Opdater en Gæst");
            Console.WriteLine("18) Søg efter Gæst udfra navn");
            Console.WriteLine("\nQ) Afslut");
        }

        public static bool Menu()
        {
            showOptions();
            switch (Console.ReadLine())
            {
                case "1":
                    ShowHotels();
                    return true;
                case "1a":
                    ShowHotelsAsync();
                    DoSomething();
                    return true;
                case "2":
                    CreateHotel();
                    return true;
                case "2a":
                    CreateHotelAsync();
                    DoSomething();
                    return true;
                case "3":
                    RemoveHotel();
                    return true;
                case "4":
                    GetHotel();
                    return true;
                case "5":
                    UpdateHotel();
                    return true;
                case "6":
                    GetHotelByName();
                    return true;
                case "7":
                    ShowRooms();
                    return true;
                case "8":
                    ShowRoomsInHotel();
                    return true;
                case "9":
                    GetRoomFromId();
                    return true;
                case "10":
                    CreateRoom();
                    return true;
                case "11":
                    UpdateRoom();
                    return true;
                case "12":
                    RemoveRoom();
                    return true;
                case "13":
                    ShowGuests();
                    return true;
                case "14":
                    CreateGuest();
                    return true;
                case "15":
                    RemoveGuest();
                    return true;
                case "16":
                    GetGuest();
                    return true;
                case "17":
                    UpdateGuest();
                    return true;
                case "18":
                    GetGuestByName();
                    return true;
                case "Q": 
                case "q": return false;
                default: return true;
            }

        }

        private async static void CreateHotelAsync()
        {
            Console.Clear();
            //indlæs data
            Console.WriteLine("Indlæs hotelNr");
            int hotelNr = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Indlæs hotelNavn");
            string hotelNavn = Console.ReadLine();

            Console.WriteLine("Indlæs adresse");
            string hotelAddresse = Console.ReadLine();

            //kalde hotelservice og vis resultatet
            HotelServiceAsync hs = new HotelServiceAsync();
            bool ok = await hs.CreateHotelAsync(new Hotel(hotelNr, hotelNavn, hotelAddresse));
            if (ok)
            {
                Console.WriteLine("Hotel oprettet");
            }
            else
            {
                Console.WriteLine("FEJL - Hotel kunne ikke oprettes");
            }
        }

        private static void GetGuestByName()
        {
            Console.Clear();
            Console.WriteLine("Indtast navn på gæst");
            string guestName = Console.ReadLine();

            GuestService gs = new GuestService();
            List<Guest> guests = gs.GetGuestsByName(guestName);
            foreach (Guest guest in guests)
            {
                Console.WriteLine($"GuestNo: {guest.GuestNo} | Name: {guest.Name} | Address: {guest.Address}");
            }
        }

        private static void DoSomething()
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(100);
                Console.WriteLine(i + " i GUI");
            }
        }
        private async static Task ShowHotelsAsync()
        {
            Console.Clear();
            HotelServiceAsync hs = new HotelServiceAsync();
            List<Hotel> hotels = await hs.GetAllHotelAsync();
            foreach (Hotel hotel in hotels)
            {
                Console.WriteLine($"HotelNr {hotel.HotelNr} Name {hotel.HotelNr} Address {hotel.Adresse}");
            }
        }


        private static void UpdateGuest()
        {
            Console.Clear();
            Console.WriteLine("Find Gæst med ID");
            int findGuest = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Indlæs Guest No");
            int guestNo = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Indlæs Guest Name");
            string guestName = Console.ReadLine();

            Console.WriteLine("Indlæs Guest Address");
            string guestAddress = Console.ReadLine();

            GuestService gs = new GuestService();
            if (gs.UpdateGuest(new Guest(guestNo, guestName, guestAddress), findGuest))
            {
                Console.WriteLine("Gæst opdateret");
            }
            else
            {
                Console.WriteLine("FEJL - Gæst kunne ikke opdateres");
            }
        }

        private static void GetGuest()
        {
            Console.Clear();

            Console.WriteLine("Indtast GuestNo");
            int guestNo = Convert.ToInt32(Console.ReadLine());

            GuestService gs = new GuestService();
            Console.WriteLine(gs.GetGuestFromId(guestNo));
        }

        private static void RemoveGuest()
        {
            Console.Clear();

            Console.WriteLine("Indtast GuestNo");
            int guestNo = Convert.ToInt32(Console.ReadLine());

            GuestService gs = new GuestService();
            Console.WriteLine($"You have deleted {gs.DeleteGuest(guestNo)}");
        }

        private static void CreateGuest()
        {
            Console.Clear();

            Console.WriteLine("Indlæs GuestNo");
            int GuestNo = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Indlæs gæst navn");
            string GuestName = Console.ReadLine();

            Console.WriteLine("Indlæs gæst adresse");
            string GuestAddress = Console.ReadLine();


            GuestService gs = new GuestService();
            if (gs.CreateGuest(new Guest(GuestNo, GuestName, GuestAddress)))
            {
                Console.WriteLine("Gæst oprettet");
            }
            else
            {
                Console.WriteLine("FEJL - Gæst kunne ikke oprettes");
            }
        }

        private static void ShowGuests()
        {
            Console.Clear();
            GuestService gs = new GuestService();
            List<Guest> guests = gs.GetAllGuests();
            foreach (Guest g in guests)
            {
                Console.WriteLine($"GuestNo: {g.GuestNo} | Name: {g.Name} | Address: {g.Address}");
            }
        }

        private static void RemoveRoom()
        {
            Console.Clear();

            Console.WriteLine("Indtast hotelNr");
            int hotelNr = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Indtast RoomNr");
            int roomNr = Convert.ToInt32(Console.ReadLine());

            RoomService rs = new RoomService();
            Console.WriteLine($"You have deleted {rs.DeleteRoom(roomNr, hotelNr)}");
        }

        private static void UpdateRoom()
        {
            Console.Clear();
            Console.WriteLine("Søg HotelNr");
            int hotelNrSearch = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("søg RoomNr");
            int roomNrSearch = Convert.ToInt32(Console.ReadLine());


            Console.WriteLine("Indlæs HotelNr");
            int hotelNr = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Indlæs RoomNr");
            int roomNr = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("indlæs room type (INDTAST ENTEN S, F eller D)");
            char type = Console.ReadLine()[0];

            Console.WriteLine("Indlæs price");
            double price = Convert.ToInt32(Console.ReadLine());

            RoomService rs = new RoomService();
            if (rs.UpdateRoom(new Room(roomNr, type, price, hotelNr), roomNrSearch, hotelNrSearch))
            {
                Console.WriteLine("Room opdateret");
            }
            else
            {
                Console.WriteLine("FEJL - Room kunne ikke opdateres");
            }
        }

        private static void CreateRoom()
        {
            Console.Clear();
            Console.WriteLine("Indlæs HotelNr");
            int hotelNr = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Indlæs RoomNr");
            int roomNr = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("indlæs room type (INDTAST ENTEN S, F eller D)");
            char type = Console.ReadLine()[0];

            Console.WriteLine("Indlæs price");
            double price = Convert.ToInt32(Console.ReadLine());


            RoomService rs = new RoomService();
            if (rs.CreateRoom(hotelNr, new Room(roomNr, type, price, hotelNr)))
            {
                Console.WriteLine("Room oprettet");
            }
            else
            {
                Console.WriteLine("FEJL - Room kunne ikke oprettes");
            }
        }

        private static void GetRoomFromId()
        {
            Console.Clear();
            Console.WriteLine("indtast Room Nr");
            int roomNr = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Indtast Hotel Nr");
            int hotelNr = Convert.ToInt32(Console.ReadLine());

            RoomService rs = new RoomService();
            HotelService hs = new HotelService();
            Room room = rs.GetRoomFromId(roomNr, hotelNr);

            Console.WriteLine($"Room Nr {room.RoomNr} in {hs.GetHotelFromId(room.HotelNr).Navn} is a {room.Types} type of room and costs {room.Pris}");
        }

        private static void ShowRoomsInHotel()
        {
            Console.Clear();
            Console.WriteLine("Indtast Hotel Nr");
            int hotelNr = Convert.ToInt32(Console.ReadLine());

            RoomService rs = new RoomService();
            List<Room> rooms = rs.GetAllRoomsInHotel(hotelNr);
            foreach (Room room in rooms)
            {
                Console.WriteLine($"RoomNr {room.RoomNr} | type {room.Types} | Price {room.Pris} | HotelNr {room.HotelNr}");
            }
        }

        private static void ShowRooms()
        {
            Console.Clear();
            RoomService rs = new RoomService();
            List<Room> rooms = rs.GetAllRooms();
            foreach (Room room in rooms)
            {
                Console.WriteLine($"RoomNr {room.RoomNr} | type {room.Types} | Price {room.Pris} | HotelNr {room.HotelNr}");
            }
        }

        private static void GetHotelByName()
        {
            Console.Clear();
            Console.WriteLine("Indtast navn på hotel");
            string hotelName = Console.ReadLine();

            HotelService hs = new HotelService();
            List<Hotel> hotels = hs.GetHotelsByName(hotelName);
            foreach (Hotel hotel in hotels)
            {
                Console.WriteLine($"HotelNr {hotel.HotelNr} Name {hotel.Navn} Address {hotel.Adresse}");
            }
        }

        private static void UpdateHotel()
        {
            Console.Clear();
            Console.WriteLine("Find hotel med ID");
            int findHotel = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Indlæs hotelNr");
            int hotelNr = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Indlæs hotelNavn");
            string hotelNavn = Console.ReadLine();

            Console.WriteLine("Indlæs adresse");
            string hotelAddresse = Console.ReadLine();

            HotelService hs = new HotelService();
            if (hs.UpdateHotel(new Hotel(hotelNr, hotelNavn, hotelAddresse), findHotel))
            {
                Console.WriteLine("Hotel opdateret");
            }
            else
            {
                Console.WriteLine("FEJL - Hotel kunne ikke opdateres");
            }

        }

        private static void RemoveHotel()
        {
            Console.Clear();

            Console.WriteLine("Indtast hotelNr");
            int hotelNr = Convert.ToInt32(Console.ReadLine());

            HotelService hs = new HotelService();
            Console.WriteLine($"You have deleted {hs.DeleteHotel(hotelNr)}");
        }

        private static void GetHotel()
        {
            Console.Clear();

            Console.WriteLine("Indtast hotelNr");
            int hotelNr = Convert.ToInt32(Console.ReadLine());

            HotelService hs = new HotelService();
            Console.WriteLine(hs.GetHotelFromId(hotelNr));
        }

        private static void CreateHotel()
        {
            Console.Clear();
            //indlæs data
            Console.WriteLine("Indlæs hotelNr");
            int hotelNr = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Indlæs hotelNavn");
            string hotelNavn = Console.ReadLine();

            Console.WriteLine("Indlæs adresse");
            string hotelAddresse = Console.ReadLine();

            //kalde hotelservice og vis resultatet
            HotelService hs = new HotelService();
            bool ok = hs.CreateHotel(new Hotel(hotelNr, hotelNavn, hotelAddresse));
            if (ok)
            {
                Console.WriteLine("Hotel oprettet");
            }
            else
            {
                Console.WriteLine("FEJL - Hotel kunne ikke oprettes");
            }
        }

        private static void ShowHotels()
        {
            Console.Clear();
            HotelService hs = new HotelService();
            List<Hotel> hotels = hs.GetAllHotel();
            foreach (Hotel hotel in hotels)
            {
                Console.WriteLine($"HotelNr {hotel.HotelNr} Name {hotel.Navn} Address {hotel.Adresse}");
            }
        }
    }
}
