using BookingApp.Entities.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace BookingApp.Persistence.Repositories
{
    public class RoomRepository
    {
        private readonly string _connectionString;
        public RoomRepository(IConfiguration configuration)
        {
            _connectionString = configuration?.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(configuration));
        }
        public void Add(Room room)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                CreateTableIfNotExists(connection);
                SqliteCommand command = new SqliteCommand($"INSERT INTO Rooms(Id, Num SquareMeters, Capacity, NumberOfBeds, Floor, PricePerNight) VALUES('{room.Id}', {room.SquareMeters}, {room.Capacity}, {room.NumberOfBeds}, {room.Floor}, {room.PricePerNight})", connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public Room GetById(string id)
        {
            Room room = new();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                //create table if not exists
                CreateTableIfNotExists(connection);

                string sql = $"SELECT Id, Num, SquareMeters, Capacity, NumberOfBeds, Floor, PricePerNight FROM Rooms WHERE Id = '{id}'";
                SqliteCommand command = new SqliteCommand(sql, connection);
                SqliteDataReader dataReader = command.ExecuteReader();

                if (dataReader.Read())
                {
                    room.Id = dataReader.GetString(0);
                    room.Num = dataReader.GetInt32(1);
                    room.SquareMeters = dataReader.GetDouble(2);
                    room.Capacity = dataReader.GetInt32(3);
                    room.NumberOfBeds = dataReader.GetInt32(4);
                    room.Floor = dataReader.GetInt32(5);
                    room.PricePerNight = dataReader.GetDouble(6);
                }

                connection.Close();
            }

            return room;
        }

        public void CreateRoom(Room room)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                //create table if not exists
                CreateTableIfNotExists(connection);

                //insert room to DB
                SqliteCommand command = new SqliteCommand($"INSERT INTO Rooms(Id, Num, SquareMeters, Capacity, NumberOfBeds, Floor, PricePerNight) VALUES('{room.Id}', {room.Num}, {room.SquareMeters}, {room.Capacity}, {room.NumberOfBeds}, {room.Floor}, {room.PricePerNight})", connection);
                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        private void CreateTableIfNotExists(SqliteConnection connection)
        {
            //create table if not exists
            string createTableQuery = "CREATE TABLE IF NOT EXISTS Rooms (Id TEXT PRIMARY KEY, TEXT, Num INTEGER, SquareMeters REAL, Capacity INTEGER, NumberOfBeds INTEGER, Floor INTEGER, PricePerNight REAL)";
            SqliteCommand createCommand = new SqliteCommand(createTableQuery, connection);
            createCommand.ExecuteNonQuery();
        }
    }
}