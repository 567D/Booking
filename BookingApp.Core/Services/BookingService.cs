using BookingApp.Entities.Models;
using BookingApp.Persistence;
using BookingApp.Persistence.Repositories;

namespace BookingApp.Core.Services
{
    public class BookingService
    {
        private readonly BookingRepository _bookingRepository;
        private readonly UserRepository _userRepository;

        public BookingService()
        {
            _bookingRepository = new BookingRepository();
            _userRepository = new UserRepository();
        }

        public string Book(string userId, int roomNum)
        {
            string bookingId = Guid.NewGuid().ToString();
            Booking booking = new Booking(bookingId, userId, roomNum);
            _bookingRepository.Add(booking);
            return $"Room number {roomNum} is booked by user {userId}, bookingId = {bookingId}";
        }

        public string CancelBookingsForUser(string userId)
        {
            User user = _userRepository.GetById(userId);
            List<Booking> bookings = _bookingRepository.GetByUserId(userId);

            foreach (var booking in bookings)
            {
                _bookingRepository.Delete(booking);
            }

            return $"All bookings for user {user.Name} has canceled";
        }

        public List<Booking> GetBookings(string userId)
        {
            List<Booking> bookings = _bookingRepository.GetByUserId(userId);
            return bookings;
        }

        public Booking Get(string id)
        {
            return _bookingRepository.Get(id);
        }
    }
}
