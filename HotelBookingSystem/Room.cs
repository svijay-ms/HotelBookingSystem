using System;

namespace HotelBookingSystem
{
    public class Room
    {
        public int Id { get; set; }

        public string GuestName { get; set; }

        public DateTime ReservedDate { get; set; }

        public bool Status { get; set; }
    }
}
