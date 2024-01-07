namespace Console.Booking.Core.Services
{
    public class BookingService
    {
        public string Book(int userId, int roomNum)
        {
            Models.Booking booking = new Models.Booking(userId, roomNum);
            return $"Room number {roomNum} is booked by user {userId}";
        }
    }
}
