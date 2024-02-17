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

        public User Get(string id)
        {
            return _userRepository.GetById(id);
        }

        public void UpdateUser(string id, string newName, string email)
        {
            var user = _userRepository.GetById(id);
            user.Name = newName;
            user.Email = email;
            _userRepository.Update(user);
        }

        public User GetUserById(string UserId)
        {
            return _userRepository.GetById(UserId);
        }

        public List<User> GetAll()
        {
            return _userRepository.GetAll().ToList();
        }

        public bool CreateUser(string name, string email)
        {
            List<User> users = _userRepository.GetAll().ToList();
            bool userIsExists = false;
            bool userCreated = false;

            foreach (User u in users)
            {
                if (u.Email.ToLower() == email.ToLower())
                {
                    userIsExists = true;
                }
            }

            if (!userIsExists)
            {
                User user = new User();
                user.Name = name;
                user.Email = email;
                user.Id = Guid.NewGuid().ToString();
                userCreated = true;
            }

            return userCreated;
        }
    }
}

