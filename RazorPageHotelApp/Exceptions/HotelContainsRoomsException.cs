using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPageHotelApp.Exceptions
{
    public class HotelContainsRoomsException:Exception
    {
        public HotelContainsRoomsException(string message):base(message)
        {
        }
    }
}
