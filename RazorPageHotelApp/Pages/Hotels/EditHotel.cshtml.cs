using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageHotelApp.Interfaces;
using RazorPageHotelApp.Models;

namespace RazorPageHotelApp.Pages.Hotels
{
    public class EditHotelModel : PageModel
    {
        [BindProperty] public Hotel NewHotel { get; set; }
        public bool done { get; set; }
        private IHotelService hotelService;
        public EditHotelModel(IHotelService hService)
        {
            hotelService = hService;
            done = false;
        }
        public async void OnGet(int hNr)
        {
            NewHotel = await hotelService.GetHotelFromIdAsync(hNr);
        }

        public void OnPost()
        {
            hotelService.UpdateHotelAsync(NewHotel, NewHotel.HotelNr);
            done = true;
        }
    }
}
