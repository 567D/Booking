using BookingApp.Core.Abstractions;
using BookingApp.Entities.Models;
using BookingApp.Persistence.Abstractions;

namespace BookingApp.Core.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;

        public BookingService(IUserRepository userRepository, IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public string Add(string userId, int roomNum)
        {
            string bookingId = Guid.NewGuid().ToString();
            Booking booking = new Booking(bookingId, userId, roomNum);
            _bookingRepository.Add(booking);

            return $"Room number {roomNum} is booked by user {userId}, bookingId = {bookingId}";
        }

        public string Cancel(string userId)
        {
            User user = _userRepository.GetById(userId);
            List<Booking> bookings = _bookingRepository.GetByUserId(userId);

            foreach (var booking in bookings)
            {
                _bookingRepository.Delete(booking);
            }

            return $"All bookings for user {user.Name} has canceled";
        }

        public List<Booking> GetForUser(string userId)
        {
            List<Booking> bookings = _bookingRepository.GetByUserId(userId);
            return bookings;
        }

        public Booking Get(string id)
        {
            return _bookingRepository.GetById(id);
        }

        public List<Booking> Get()
        {
            return _bookingRepository.GetAll().ToList();
        }
    }
}

