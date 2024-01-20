using BookingApp.Entities.Models;
using BookingApp.Persistence;
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

        public void CancelBooking(int userId, int roomNum)
        {
            var bookings = _bookingRepository.GetByUser(userId);
            Booking bookingToRemove = bookings.FirstOrDefault(x => x.UserId == userId && x.RoomNum == roomNum);

            if (bookingToRemove != null)
            {
                _bookingRepository.Remove(bookingToRemove);
                Console.WriteLine($"for room number {roomNum} by user {userId} is canceled");
            }
            else
            {
                Console.WriteLine($"for room number {roomNum} by user {userId} not found");
            }
        }

        public List<Booking> GetBookings(int userId)
        {
            var bookings = _bookingRepository.GetByUser(userId);
            return bookings;
        }
    }
}
