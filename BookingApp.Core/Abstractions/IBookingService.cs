using BookingApp.Entities.Models;

namespace BookingApp.Core.Abstractions
{
	public interface IBookingService
	{
        public string Add(string userId, int roomNum);
        public string Cancel(string userId);
        public List<Booking> GetForUser(string userId);
        public Booking Get(string id);
        public List<Booking> Get();
    }
}

