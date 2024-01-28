using System;
using BookingApp.Entities.Models;
using Microsoft.Data.Sqlite;
using BookingApp.Persistence.Interfaces;

namespace BookingApp.Persistence.Repositories
{
    public class BookingRepository : IRepository<Booking>
    {
        private string _dbConnection = "/Users/araks/programming/Booking/bookingAppDb.db";

        public void Add(Booking entity)
        {
            using (var connection = new SqliteConnection($"Data source={_dbConnection}"))
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
            using (var connection = new SqliteConnection($"Data source={_dbConnection}"))
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
            using (var connection = new SqliteConnection($"Data source={_dbConnection}"))
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
            using (var connection = new SqliteConnection($"Data source={_dbConnection}"))
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
            using (var connection = new SqliteConnection($"Data source={_dbConnection}"))
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
            using (var connection = new SqliteConnection($"Data source={_dbConnection}"))
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

        private void CreateTableIfNotExists(SqliteConnection connection)
        {
            //create table if not exists
            string createTableQuery = "CREATE TABLE IF NOT EXISTS Bookings (Id TEXT, UserId TEXT NOT NULL, RoomNum TEXT NOT NULL, PRIMARY KEY(Id))";
            SqliteCommand createCommand = new SqliteCommand(createTableQuery, connection);
            createCommand.ExecuteNonQuery();
        }
    }
}

