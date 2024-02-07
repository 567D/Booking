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

        public void CreateUser(string id, string name)
        {
            var user = new User();
            user.Id = id;
            _userRepository.Add(user);
        }

        public void UpdateUser(string id, string newName)
        {
            var user = _userRepository.GetById(id);
            user.Name = newName;
            _userRepository.Update(user);
        }

        public User GetUserById(string UserId)
        {
            return _userRepository.GetById(UserId);
        }
	}
}

