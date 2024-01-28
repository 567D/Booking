using BookingApp.Core.Services;
using BookingApp.Entities.Models;

public class Program
{
    private static void ShowBookings(List<Booking> bookings)
    {
        foreach (var booking in bookings)
        {
            Console.WriteLine($"User with id = {booking.UserId} has booked room with id = {booking.RoomNum}");
        }
    }
    public static void Main(string[] args)
    {
        //define variables
        int userId = 1;
        int room1Id = 1;
        int room2Id = 2;
        int room3Id = 3;

        //create service class object
        BookingService service = new BookingService();

        

        //service.CancelBooking(userId, room1Id);
        //service.CancelBooking(userId, room3Id);

        //put empty line delimiter
        //Console.WriteLine("_________________");

        //get bookings by user id = 1
        List<Booking> bookings = service.GetBookings(userId);

        //show bookings from user with id = 1
        
        //loop request
        bool ExitRequest = false;
        while (ExitRequest == false)
        {
            Console.WriteLine("Enter a command (create, get, cancel, exit):");
            string UserInput = Console.ReadLine();

            if (UserInput == "create")
            {
                //book room with number = 1 by user with id = 1
                Console.WriteLine(service.Book(userId, room1Id));
                //book room with number = 2 by user with id = 1
                Console.WriteLine(service.Book(userId, room2Id));
                //book room with number = 3 by user with id = 1
                Console.WriteLine(service.Book(userId, room3Id));
            }
            if (UserInput == "get")
            {
                bookings = service.GetBookings(userId);
                ShowBookings(bookings);
            }
            if (UserInput == "cancel")
            {
                service.CancelBooking(userId, room1Id);
                service.CancelBooking(userId, room3Id);

                //put empty line delimiter
                Console.WriteLine("_________________");

            }
            if (UserInput == "exit")
            {
                ExitRequest = true;
                Console.WriteLine("Exit");
            }
        }
        //Success message, program has finished successfully
        Console.WriteLine("The program has finished!!!");
    }
}