using System.Collections.Generic;
using BookingApp.Entities.Models;
using Microsoft.Data.Sqlite;

namespace BookingApp.Persistence
{
    public class Database
    {
        private string _dbConnection = "/Users/araks/programming/Booking/bookingAppDb.db";

        public Database()
        {
            Bookings = new List<Booking>();
        }
        
        public List<Booking> Bookings { get; set; }

        public void AddUser(User user)
        {
            using(var connection = new SqliteConnection($"Data source={_dbConnection}"))
            {
                connection.Open();

                //create table if not exists
                string createTableQuery = "CREATE TABLE IF NOT EXISTS Users (Id TEXT, Name TEXT NOT NULL, PRIMARY KEY(Id))";
                SqliteCommand createCommand = new SqliteCommand(createTableQuery, connection);
                

                createCommand.ExecuteNonQuery();

                //insert user to DB
                SqliteCommand command = new SqliteCommand("INSERT INTO Users(Id, Name) VALUES('" + user.Id + "', '" + user.Name + "')", connection);
                
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}