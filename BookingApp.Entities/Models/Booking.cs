namespace BookingApp.Entities.Models
{
    public class Booking
    {
        public string UserId { get; set; }
        public int RoomNum { get; set; }

        public Booking(string userId, int roomNum)
        {
            UserId = userId;
            RoomNum = roomNum;
        }
    }
}
