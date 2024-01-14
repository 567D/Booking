using BookingApp.Entities.Models;
using BookingApp.Persistence.Repositories;

namespace BookingApp.Core.Services
{
    public class BookingService
    {
        private readonly BookingRepository _bookingRepository;
        
        public BookingService()
        {
            _bookingRepository = new BookingRepository();
        }

        public string Book(int userId, int roomNum)
        {
            Booking booking = new Booking(userId, roomNum);
            _bookingRepository.Add(booking);
            return $"Room number {roomNum} is booked by user {userId}";
        }

        public List<Booking> GetBookings(int userId)
        {
            var bookings = _bookingRepository.GetByUser(userId);
            return bookings;
        } 
    }
}
