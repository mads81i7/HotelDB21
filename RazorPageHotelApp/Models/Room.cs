using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPageHotelApp.Models
{
    /// <summary>
    /// Denne klasse repræsenterer et simpelt værelse,
    /// som indeholder et RoomNr, Types, Pris og et HotelNr
    /// </summary>
    public class Room
    {
        public int RoomNr { get; set; }

        [Required]
        public char Types { get; set; }

        [Required]
        [Range(0, 9999)]
        public double Pris { get; set; }
        public int HotelNr { get; set; }

        public Room()
        {

        }
        public Room(int nr, char types, double pris)
        {
            RoomNr = nr;
            Types = types;
            Pris = pris;
        }

        public Room(int nr, char types, double pris, int hotelNr) : this(nr, types, pris)
        {
            HotelNr = hotelNr;
        }

        public override string ToString()
        {
            return $"Room = {RoomNr}, Types = {Types}, Pris = {Pris}";
        }
    }
}
