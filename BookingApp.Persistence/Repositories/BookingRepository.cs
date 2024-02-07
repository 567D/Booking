using BookingApp.Entities.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using BookingApp.Persistence.Abstractions;

namespace BookingApp.Persistence.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly string _connectionString;

        public BookingRepository(IConfiguration configuration)
        {
            _connectionString = configuration?.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void Add(Booking entity)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                //create table if not exists
                CreateTableIfNotExists(connection);

                //insert user to DB
                SqliteCommand command = new SqliteCommand("INSERT INTO Bookings(Id, UserId, RoomNum) VALUES('" + entity.Id + "', '" + entity.UserId + "', '" + entity.RoomNum + "')", connection);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void Delete(Booking entity)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                //create table if not exists
                CreateTableIfNotExists(connection);

                //insert user to DB
                SqliteCommand command = new SqliteCommand("DELETE FROM Bookings where Id = '" + entity.Id + "'", connection);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public IEnumerable<Booking> GetAll()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                //create table if not exists
                CreateTableIfNotExists(connection);

                string sql = "select * from Bookings";
                SqliteCommand command = new SqliteCommand(sql, connection);
                SqliteDataReader dataReader = command.ExecuteReader();

                List<Booking> bookings = new List<Booking>();

                while (dataReader.Read())
                {
                    bookings.Add(new Booking(dataReader.GetString(0), dataReader.GetString(1), int.Parse(dataReader.GetString(2))));
                }

                connection.Close();
                return bookings;
            }
        }

        public Booking GetById(string id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                //create table if not exists
                CreateTableIfNotExists(connection);

                string sql = "select * from Bookings where Id = '" + id + "'";
                SqliteCommand command = new SqliteCommand(sql, connection);
                SqliteDataReader dataReader = command.ExecuteReader();

                List<Booking> bookings = new List<Booking>();

                while (dataReader.Read())
                {
                    bookings.Add(new Booking(dataReader.GetString(0), dataReader.GetString(1), int.Parse(dataReader.GetString(2))));
                }

                connection.Close();
                return bookings.FirstOrDefault();
            }
        }

        public void Update(Booking entity)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                //create table if not exists
                CreateTableIfNotExists(connection);

                //insert user to DB
                SqliteCommand command = new SqliteCommand("UPDATE Bookings SET UserId = '" + entity.UserId + "', RoomNum = '" + entity.RoomNum + "' where Id = '" + entity.Id + "'", connection);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<Booking> GetByUserId(string userId)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                //create table if not exists
                CreateTableIfNotExists(connection);

                string sql = "select * from Bookings where UserID = '" + userId + "'";
                SqliteCommand command = new SqliteCommand(sql, connection);
                SqliteDataReader dataReader = command.ExecuteReader();

                List<Booking> bookings = new List<Booking>();

                while (dataReader.Read())
                {
                    bookings.Add(new Booking(dataReader.GetString(0), dataReader.GetString(1), int.Parse(dataReader.GetString(2))));
                }

                connection.Close();
                return bookings;
            }
        }

        public Booking Get(string id)
        {
            Booking booking = new();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                //create table if not exists
                CreateTableIfNotExists(connection);

                string sql = $"select Id, UserId, RoomNum from Bookings where Id ='{id}'";
                SqliteCommand command = new SqliteCommand(sql, connection);

                //we have data in memory at this moment
                SqliteDataReader dataReader = command.ExecuteReader();

                

                if(dataReader.Read())
                {
                    booking.Id = dataReader.GetString(0);
                    booking.UserId = dataReader.GetString(1);
                    booking.RoomNum = int.Parse(dataReader.GetString(2));
                }
            }

            return booking;
        }

        private void CreateTableIfNotExists(SqliteConnection connection)
        {
            //create table if not exists
            string createTableQuery = "CREATE TABLE IF NOT EXISTS Bookings (Id TEXT, UserId TEXT NOT NULL, RoomNum TEXT NOT NULL, PRIMARY KEY(Id))";
            SqliteCommand createCommand = new SqliteCommand(createTableQuery, connection);
            createCommand.ExecuteNonQuery();
        }
    }
}
