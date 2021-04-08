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
    public class EditRoomModel : PageModel
    {
        [BindProperty] public Room Room { get; set; }
        public bool done { get; set; }
        private IRoomService roomService;
        public char[] RoomTypes = new[] { 'S', 'D', 'F' };
        public string[] TypeNames = new[] { "Single", "Double", "Family" };
        public EditRoomModel(IRoomService rService)
        {
            roomService = rService;
            done = false;
        }
        public async void OnGet(int hNr, int rNr)
        {
            Room = await roomService.GetRoomFromIdAsync(rNr, hNr);
        }

        public void OnPost(int hNr)
        {
            roomService.UpdateRoomAsync(Room, Room.RoomNr, Room.HotelNr);
            done = true;
        }
    }
}
