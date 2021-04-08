using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop.Infrastructure;
using RazorPageHotelApp.Interfaces;
using RazorPageHotelApp.Models;

namespace RazorPageHotelApp.Pages.Rooms
{
    public class CreateRoomModel : PageModel
    {
        private IRoomService roomService;
        [BindProperty] public Room NewRoom { get; set; }
        [BindProperty] public char RoomType { get; set; }
        public char[] RoomTypes = new[] {'S', 'D', 'F'};
        public string[] TypeNames = new[] {"Single", "Double", "Family"};
        public bool done { get; set; }

        public CreateRoomModel(IRoomService rService)
        {
            roomService = rService;
            NewRoom = new Room();
            done = false;
        }
        public void OnGet(int hNr)
        {
            NewRoom.HotelNr = hNr;
        }

        public async Task<IActionResult> OnPostAsync(int hNr)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await roomService.CreateRoomAsync(hNr, NewRoom);
            //done = true;
            return RedirectToPage("/Rooms/GetAllRooms", new {hNr});
        }
    }
}
