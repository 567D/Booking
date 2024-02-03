namespace BookingApp.Entities.Models
{
    public class Booking
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public int RoomNum { get; set; }

        public Booking()
        {
        }

        public Booking(string id, string userId, int roomNum)
        {
            Id = id;
            UserId = userId;
            RoomNum = roomNum;
        }
    }
}
