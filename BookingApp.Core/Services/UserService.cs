using BookingApp.Core.Abstractions;
using BookingApp.Entities.Models;
using BookingApp.Persistence.Abstractions;

namespace BookingApp.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
		{
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public void Add(string id, string name)
        {
            var user = new User();
            user.Id = id;
            user.Name = name;
            _userRepository.Add(user);
        }

        public User Get(string id)
        {
            return _userRepository.GetById(id);
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

