using BookingApp.Entities.Models;

namespace BookingApp.Core.Services
{
    public class BookingService
    {
        public string Book(int userId, int roomNum)
        {
            Booking booking = new Booking(userId, roomNum);
            return $"Room number {roomNum} is booked by user {userId}";
        }
    }
}
