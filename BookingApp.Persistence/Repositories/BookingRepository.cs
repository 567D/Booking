using BookingApp.Entities.Models;

namespace BookingApp.Persistence.Repositories
{
    public class BookingRepository
    {
        private readonly Database _database;

        public BookingRepository()
        {
            _database = new Database();
        }

        public void Add(Booking booking)
        {
            _database.Bookings.Add(booking);
        }

        public List<Booking> GetByUser(int userId)
        {
            return _database.Bookings.Where(b => b.UserId == userId).ToList();
        }
    }
}