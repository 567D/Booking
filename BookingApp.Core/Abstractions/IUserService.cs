using BookingApp.Entities.Models;

namespace BookingApp.Core.Abstractions
{
	public interface IUserService
	{
		public void CreateUser(string id, string name, string email);
		public User Get(string id);
		public void UpdateUser(string id, string newName, string email);
		public List<User> GetAll();
    }
}

