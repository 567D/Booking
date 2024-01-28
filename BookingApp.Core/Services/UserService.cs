using System;
using BookingApp.Entities.Models;
using BookingApp.Persistence.Repositories;

namespace BookingApp.Core.Services
{
	public class UserService
	{
        private readonly UserRepository _userRepository;

        public UserService()
		{
            _userRepository = new UserRepository();
        }

		public void AddUser(string id, string name)
		{
            var user = new User();
            user.Id = id;
            user.Name = name;
            _userRepository.Add(user);
        }

        public User Get(string userid)
        {
            return _userRepository.GetById(userid);
        }
	}
}

