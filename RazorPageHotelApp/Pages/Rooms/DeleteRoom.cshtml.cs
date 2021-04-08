using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageHotelApp.Interfaces;
using RazorPageHotelApp.Models;

namespace RazorPageHotelApp.Pages.Rooms
{
    public class DeleteRoomModel : PageModel
    {
        private IRoomService roomService;
        [BindProperty] public Room Room { get; set; }

        public DeleteRoomModel(IRoomService rService)
        {
            roomService = rService;
        }
        public async void OnGet(int hNr, int rNr)
        {
            Room = await roomService.GetRoomFromIdAsync(rNr, hNr);
        }

        public IActionResult OnPostConfirm(int hNr, int rNr)
        {
            roomService.DeleteRoomAsync(rNr, hNr);
            //return RedirectToPage("/Hotels/GetAllHotels");
            return RedirectToPage("GetAllRooms", new {hNr});
        }
    }
}
