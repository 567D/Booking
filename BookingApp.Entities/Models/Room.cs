namespace BookingApp.Entities.Models
{
    public class Room
    {
        public string Id { get; set; }
        public int Num { get; set; }
        public double SquareMeters { get; set; }
        public int NumberOfBeds { get; set; }
        public double PricePerNight { get; set; }
        public int Capacity { get; set; }
        public int Floor { get; set; }
    }
}