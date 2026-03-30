using RentalApp.Models;

namespace RentalApp.Data;

public interface IUserRepository
{
    void Add(User user);
    User GetById(int id);
    IEnumerable<User> GetAll();
}