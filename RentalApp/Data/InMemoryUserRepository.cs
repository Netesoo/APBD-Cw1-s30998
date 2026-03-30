namespace RentalApp.Data;

using RentalApp.Models;

public class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _users = new List<User>();

    public void Add(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        if (_users.Any(u => u.Id == user.Id))
        {
            throw new InvalidOperationException("Użytkownik o takim ID już istnieje w systemie.");
        }

        _users.Add(user);
    }

    public User GetById(int id)
    {
        return _users.FirstOrDefault(u => u.Id == id);
    }

    public IEnumerable<User> GetAll()
    {
        return _users.AsReadOnly();
    }
}