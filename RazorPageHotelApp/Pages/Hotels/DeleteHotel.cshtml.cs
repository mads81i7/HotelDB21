using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageHotelApp.Exceptions;
using RazorPageHotelApp.Interfaces;
using RazorPageHotelApp.Models;

namespace RazorPageHotelApp.Pages.Hotels
{
    public class DeleteHotelModel : PageModel
    {
        private IHotelService hotelService;
        private IRoomService roomService;
        [BindProperty] public Hotel Hotel { get; set; }
        public List<Room> RoomsInHotel { get; set; }
        public string ErrorMessage { get; set; }

        public DeleteHotelModel(IHotelService hService, IRoomService rService)
        {
            hotelService = hService;
            roomService = rService;
        }
        public async void OnGet(int hNr)
        {
            Hotel = await hotelService.GetHotelFromIdAsync(hNr);
        }

        public async Task<IActionResult> OnPostConfirm(int hNr)
        {
            RoomsInHotel = roomService.GetAllRoomsInHotelAsync(hNr).Result;

            try
            {
                await hotelService.DeleteHotelAsync(hNr, RoomsInHotel);
            }
            catch (HotelContainsRoomsException he)
            {
                ErrorMessage = he.Message;
                return Page();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ErrorMessage = e.Message;
                OnGet(hNr);
            }
            return RedirectToPage("GetAllHotels");
        }
        public IActionResult OnPostDeny()
        {
            return RedirectToPage("GetAllHotels");
        }
    }
}
