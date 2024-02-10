using BookingApp.Core.Abstractions;
using BookingApp.Core.Services;
using BookingApp.Entities.Models;
using BookingApp.Persistence.Abstractions;
using BookingApp.Persistence.Repositories;
using BookingApp.UI.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

public class Program
{
    public static void Main(string[] args)
    {
        ConfigurationBuilder builder = new ConfigurationBuilder();
        ServiceProvider serviceProvider = ConfigureServices(builder, new ServiceCollection(), args);
        ConfigureLogger(builder);

        var userService = serviceProvider.GetService<IUserService>();
        var bookingService = serviceProvider.GetService<IBookingService>();
        var userId = Guid.NewGuid().ToString();
        string email = "pavel@gmail.com";
        string name = "Pavel";
        List<User> users = userService.GetAll();

        bool userIsExists = false;

        foreach (User u in users)
        {
            if(u.Email.ToLower() == email.ToLower())
            {
                userIsExists = true;
            }
        }

        if (!userIsExists)
        {
            userService.Add(userId, name, email);
            User user = userService.Get(userId);
            Console.WriteLine("User created successfully");
            Console.WriteLine($"Name  = {user.Name}, id = {user.Id}, Email = { user.Email}");
        }
        else
        {
            Console.WriteLine($"User {name} with email {email} has already existed");    
        }

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
                        List<Booking> getBookings = bookingService.GetForUser(getUserId);

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
                        string result = bookingService.Add(createUserId, createRoomNum);
                        Console.WriteLine(result);
                        break;
                    }
                case UICommands.CANCEL:
                    {
                        var cancelUserId = commandText.Split(' ')[1];
                        bookingService.Cancel(userId);
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

    private static ServiceProvider ConfigureServices(IConfigurationBuilder builder, IServiceCollection services, string[] args)
    {
        IConfiguration configuration = builder
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .AddCommandLine(args)
            .Build();

        services.AddSingleton(configuration);
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IBookingRepository, BookingRepository>();
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<IBookingService, BookingService>();
        services.AddLogging(x => x.AddSerilog());
        //add new DI dependecies here

        return services.BuildServiceProvider();
    }

    private static void ConfigureLogger(ConfigurationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Build())
            .Enrich.FromLogContext()
            .WriteTo.Console(theme: AnsiConsoleTheme.Sixteen)
            .CreateLogger();

        Log.Logger.Information("BookingApp console started");
    }
}

