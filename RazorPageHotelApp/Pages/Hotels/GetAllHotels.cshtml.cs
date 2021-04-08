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
    public class GetAllHotelsModel : PageModel
    {
        [BindProperty(SupportsGet = true)] public string FilterCriteria { get; set; }
        public List<Hotel> Hotels { get; private set; }

        private IHotelService hotelService;

        public GetAllHotelsModel(IHotelService hService)
        {
            this.hotelService = hService;
            Hotels = hotelService.GetAllHotelAsync().Result;
        }
        public async Task OnGetAsync()
        {
            if (!String.IsNullOrEmpty(FilterCriteria))
            {
                Hotels = await hotelService.GetHotelsByNameAsync(FilterCriteria);
            }
            else
                Hotels = await hotelService.GetAllHotelAsync();
        }
    }

}
