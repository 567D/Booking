using BookingApp.Core.Services;
using BookingApp.Entities.Models;
using BookingApp.UI.Constants;

public class Program
{
    public static void Main(string[] args)
    {
        var userService = new UserService();
        var bookingService = new BookingService();
        var userId = Guid.NewGuid().ToString();

        userService.AddUser(userId, "Dariya");

        User user = userService.Get(userId);
        Console.WriteLine("User created successfully");
        Console.WriteLine($"Name  = {user.Name}, id = {user.Id}");
        Console.WriteLine();

        bool exit = false;

        while(!exit)
        {
            Console.WriteLine();
            Console.WriteLine("Enter a command (create, get, getbooking, cancel, exit):");
            Console.WriteLine();
            string commandText = Console.ReadLine().ToLower();
            string command = commandText.Split(' ')[0];

            switch (command)
            {
                case UICommands.GET:
                    {
                        var getUserId = commandText.Split(' ')[1];
                        List<Booking> getBookings = bookingService.GetBookings(getUserId);

                        foreach (var booking in getBookings)
                        {
                            Console.WriteLine($"Booking Id = {booking.Id}, RoomNum = {booking.RoomNum}, UserId = {booking.UserId}");
                        }

                        break;
                    }
                case UICommands.GETBOOKING:
                    {
                        var bookingId = commandText.Split(' ')[1];
                        Booking booking = bookingService.Get(bookingId);
                        Console.WriteLine($"Booking Id = {booking.Id}, RoomNum = {booking.RoomNum}, UserId = {booking.UserId}");
                        break;
                    }
                case UICommands.CREATE:
                    {
                        var createUserId = commandText.Split(' ')[1];
                        var createRoomNum = int.Parse(commandText.Split(' ')[2]);
                        string result = bookingService.Book(createUserId, createRoomNum);
                        Console.WriteLine(result);
                        break;
                    }
                case UICommands.CANCEL:
                    {
                        var cancelUserId = commandText.Split(' ')[1];
                        bookingService.CancelBookingsForUser(userId);
                        break;
                    }
                case UICommands.EXIT:
                    {
                        exit = true;
                        Console.WriteLine($"{nameof(Booking)} {UICommands.GET}");
                        break;
                    }
                default:
                    {
                        Console.WriteLine($"Unknown command, please retry with {UICommands.GET}, {UICommands.CREATE}, {UICommands.CANCEL}, {UICommands.EXIT} command");
                        Console.WriteLine();
                        break;
                    }
            }
        }

        //Success message, program has finished successfully
        Console.WriteLine("The program has finished!!!");
        Console.ReadLine();
    }

    private static void ShowBookings(List<Booking> bookings)
    {
        foreach (var booking in bookings)
        {
            Console.WriteLine($"User with id = {booking.UserId} has booked room with id = {booking.RoomNum}");
        }
    }
}