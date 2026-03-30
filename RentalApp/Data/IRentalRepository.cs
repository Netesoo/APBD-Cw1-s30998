using RentalApp.Models;

namespace RentalApp.Data;

public interface IRentalRepository
{
    void Add(Rental rental);
    Rental GetById(int id);
    IEnumerable<Rental>  GetAll();
    IEnumerable<Rental> GetActiveRentalsForUser(int userId);
    IEnumerable<Rental> GetOverdueRentals();
}