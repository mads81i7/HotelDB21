using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPageHotelApp.Models
{
    /// <summary>
    /// Denne klasse repræsenterer et simpelt Hotel,
    /// som indeholder et HotelNr, et Navn og en Adresse
    /// </summary>
    public class Hotel
    {
        
        public int HotelNr { get; set; }
        
        [Required]
        [StringLength(30)]
        public String Navn { get; set; }

        [Required]
        [StringLength(50)]
        public String Adresse { get; set; }

        public Hotel()
        {
        }
        
        public Hotel(int hotelNr, string navn, string adresse)
        {
            HotelNr = hotelNr;
            Navn = navn;
            Adresse = adresse;
        }

        public override string ToString()
        {
            return $"{nameof(HotelNr)}: {HotelNr}, {nameof(Navn)}: {Navn}, {nameof(Adresse)}: {Adresse}";
        }
    }
}
