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
    public class GetAllRoomsModel : PageModel
    {
        [BindProperty(SupportsGet = true)] public string FilterCriteria { get; set; }
        public List<Hotel> Hotels { get; private set; }
        public List<Room> Rooms { get; private set; }
        public Hotel Hotel { get; set; }
        public bool Confirm { get; set; }
        public char[] RoomTypes = new[] { 'S', 'D', 'F' };
        public string[] TypeNames = new[] { "Single", "Double", "Family" };
        [BindProperty(SupportsGet = true)] public char FilterType { get; set; }
        [BindProperty(SupportsGet = true)] public double FilterPrice { get; set; }
        [BindProperty(SupportsGet = true)] public string OrderMethod { get; set; }
        [BindProperty(SupportsGet = true)] public string OrderDirection { get; set; }

        private IHotelService hotelService;
        private IRoomService roomService;

        public GetAllRoomsModel(IHotelService hService, IRoomService rService)
        {
            hotelService = hService;
            roomService = rService;
            OrderMethod = "Room_No";
            OrderDirection = " ASC";
        }
        public async Task OnGetAsync(int hNr)
        {
            Hotel = await hotelService.GetHotelFromIdAsync(hNr);
            if (FilterType != '\0')
            {
                //Rooms = await roomService.GetAllRoomsInHotelWithTypeAsync(hNr, FilterType);
                Rooms = await roomService.GetAllRoomsInHotelSortedWithTypeAsync(hNr, FilterType, OrderMethod, OrderDirection);
            }
            else
            {
                //Rooms = await roomService.GetAllRoomsInHotelAsync(hNr);
                Rooms = await roomService.GetAllRoomsInHotelSortedAsync(hNr, OrderMethod, OrderDirection);
            }
            
            Confirm = false;
        }

        public async Task<IActionResult> OnPostDeleteAllAsync(int hNr)
        {
            Confirm = true;
            Hotel = await hotelService.GetHotelFromIdAsync(hNr);
            Rooms = await roomService.GetAllRoomsInHotelAsync(hNr);
            return Page();
        }
        public async Task<IActionResult> OnPostConfirmAsync(int hNr)
        {
            foreach (Room r in roomService.GetAllRoomsInHotelAsync(hNr).Result)
            {
                await roomService.DeleteRoomAsync(r.RoomNr, r.HotelNr);
            }
            Hotel = await hotelService.GetHotelFromIdAsync(hNr);
            Rooms = await roomService.GetAllRoomsInHotelAsync(hNr);
            return Page();
        }

        public async Task OnPostFilterAsync(int hNr)
            {
            await OnGetAsync(hNr);
            //return RedirectToPage("GetAllRooms", new {hNr, FilterType});
        }
    }
}
