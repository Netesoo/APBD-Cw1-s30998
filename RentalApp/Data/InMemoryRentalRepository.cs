namespace RentalApp.Data;

using RentalApp.Models;

public class InMemoryRentalRepository : IRentalRepository
{
    private readonly List<Rental> _rentals = new List<Rental>();

    public void Add(Rental rental)
    {
        if (rental == null)
        {
            throw new ArgumentNullException(nameof(rental));
        }

        _rentals.Add(rental);
    }

    public Rental GetById(int id)
    {
        return _rentals.FirstOrDefault(r => r.Id == id);
    }

    public IEnumerable<Rental> GetAll()
    {
        return _rentals.AsReadOnly();
    }

    public IEnumerable<Rental> GetActiveRentalsForUser(int userId)
    {
        return _rentals
            .Where(r => r.User.Id == userId && r.IsActive)
            .ToList();
    }

    public IEnumerable<Rental> GetOverdueRentals()
    {
        return _rentals
            .Where(r => r.IsOverdue)
            .ToList();
    }
}