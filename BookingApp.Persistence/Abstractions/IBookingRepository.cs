using BookingApp.Entities.Models;

namespace BookingApp.Persistence.Abstractions
{
    public interface IBookingRepository : IRepository<Booking>
	{
        public List<Booking> GetByUserId(string userId);
    }
}

