using Console.Booking.Core.Services;
public class Program
{
    public static void Main(string[] args)
    {
        BookingService service = new BookingService();
        string result = service.Book(1, 1);

        System.Console.WriteLine(result);
    }
}