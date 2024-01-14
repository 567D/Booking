using BookingApp.Entities.Models;

namespace BookingApp.Persistence
{
    public class Database
    {
        public Database()
        {
            Bookings = new List<Booking>();
        }
        
        public List<Booking> Bookings { get; set; }
    }
}