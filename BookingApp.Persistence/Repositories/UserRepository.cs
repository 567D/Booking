using System;
using BookingApp.Entities.Models;

namespace BookingApp.Persistence.Repositories
{
	public class UserRepository
	{
        private readonly Database _database;

        public UserRepository()
		{
			_database = new Database();
		}

		public void Add(User user)
		{
			_database.AddUser(user);
		}
	}
}

