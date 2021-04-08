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
    public class CreateHotelModel : PageModel
    {
        [BindProperty] public Hotel NewHotel { get; set; }
        public bool done { get; set; }    
        private IHotelService hotelService;
        public CreateHotelModel(IHotelService hService)
        {
            hotelService = hService;
            NewHotel = new Hotel();
            done = false;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await hotelService.CreateHotelAsync(NewHotel);
            done = true;
            return Page();
        }

    }
}
