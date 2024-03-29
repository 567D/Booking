﻿using BookingApp.Entities.Models;
using BookingApp.Persistence.Abstractions;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BookingApp.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(IConfiguration configuration, ILogger<UserRepository> logger)
        {
            _connectionString = configuration?.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Add(User entity)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                //create table if not exists
                CreateTableIfNotExists(connection);

                //insert user to DB
                SqliteCommand command = new SqliteCommand("INSERT INTO Users(Id, Name, Email) VALUES('" + entity.Id + "', '" + entity.Name + "', '" + entity.Email + "')", connection);

                command.ExecuteNonQuery();
                connection.Close();
            }

            _logger.LogInformation($"{nameof(User)} with name: {entity.Name} added to db");
        }

        public void Delete(User entity)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                //create table if not exists
                CreateTableIfNotExists(connection);

                //insert user to DB
                SqliteCommand command = new SqliteCommand("DELETE FROM Users where Id = '" + entity.Id + "'", connection);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public IEnumerable<User> GetAll()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                //create table if not exists
                CreateTableIfNotExists(connection);

                string sql = "select * from Users";
                SqliteCommand command = new SqliteCommand(sql, connection);
                SqliteDataReader dataReader = command.ExecuteReader();

                List<User> users = new List<User>();
                
                while (dataReader.Read())
                {
                    users.Add(new User
                    {
                        Id = dataReader.GetString(0),
                        Name = dataReader.GetString(1),
                        Email = dataReader.GetString(2)
                    });
                }

                connection.Close();
                return users;
            }
        }

        public User GetById(string id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                //create table if not exists
                CreateTableIfNotExists(connection);

                string sql = "select * from Users where Id = '"+ id +"'";
                SqliteCommand command = new SqliteCommand(sql, connection);
                SqliteDataReader dataReader = command.ExecuteReader();

                List<User> users = new List<User>();

                while (dataReader.Read())
                {
                    users.Add(new User
                    {
                        Id = dataReader.GetString(0),
                        Name = dataReader.GetString(1),
                        Email = dataReader.GetString(2)
                    });
                }

                connection.Close();
                return users.FirstOrDefault();
            }
        }

        public void Update(User entity)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                //create table if not exists
                CreateTableIfNotExists(connection);

                //insert user to DB
                SqliteCommand command = new SqliteCommand("UPDATE Users SET Name = '" + entity.Name + "', Email = '" + entity.Email +"' where Id = '" + entity.Id + "'", connection);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<User> GetUsersByRoomNum(int roomNum)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                CreateTableIfNotExists(connection);
                string sql = $"select u.Id, u.Name from Users u join Bookings b on u.Id = b.UserId where b.RoomNum = '{roomNum}'";
                SqliteCommand command = new SqliteCommand(sql, connection);
                SqliteDataReader dataReader = command.ExecuteReader();
                List<User> userList = new List<User>();
                while (dataReader.Read())
                {
                    userList.Add(new User
                    {
                        Id = dataReader.GetString(0),
                        Name = dataReader.GetString(1),
                        Email = dataReader.GetString(2)
                    });
                }
                connection.Close();
                return userList;
            }
        }

        private void CreateTableIfNotExists(SqliteConnection connection)
        {
            //create table if not exists
            string createTableQuery = "CREATE TABLE IF NOT EXISTS Users (Id TEXT, Name TEXT NOT NULL, Email TEXT NOT NULL, PRIMARY KEY(Id))";
            SqliteCommand createCommand = new SqliteCommand(createTableQuery, connection);
            createCommand.ExecuteNonQuery();
        }
    }
}

