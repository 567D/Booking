using BookingApp.Core.Services;
using BookingApp.Entities.Models;

public class Program
{
    public static void Main(string[] args)
    {
        //define variables
        string userId = "1";
        int room1Id = 1;
        int room2Id = 2;
        int room3Id = 3;



        //create service for bookings
        BookingService bookingService = new BookingService();

        //create service for users
        UserService userService = new UserService();

        //creation user in real database
        userService.AddUser(Guid.NewGuid().ToString(), "Daria");

        //book room with number = 1 by user with id = 1
        Console.WriteLine(bookingService.Book(userId, room1Id));
        //book room with number = 2 by user with id = 1
        Console.WriteLine(bookingService.Book(userId, room2Id));
        //book room with number = 3 by user with id = 1
        Console.WriteLine(bookingService.Book(userId, room3Id));

        bookingService.CancelBooking(userId, room1Id);
        bookingService.CancelBooking(userId, room3Id);

        //put empty line delimiter
        Console.WriteLine("_________________");

        //get bookings by user id = 1
        List<Booking> bookings = bookingService.GetBookings(userId);

        //show bookings from user with id = 1
        foreach (var booking in bookings)
        {
            Console.WriteLine($"User with id = {booking.UserId} has booked room with id = { booking.RoomNum }");
        }

        //Success message, program has finished successfully
        Console.WriteLine("The program has finished!!!");
        Console.ReadLine();
    }
}