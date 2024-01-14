namespace BookingApp.Entities.Models
{
    public class Booking
    {
        public int UserId { get; set; }
        public int RoomNum { get; set; }

        public Booking(int userId, int roomNum)
        {
            UserId = userId;
            RoomNum = roomNum;
        }
    }
}
