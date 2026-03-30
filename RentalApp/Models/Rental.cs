using RentalApp.Exceptions;

namespace RentalApp.Models;

public class Rental(User user, Equipment equipment, DateTime date, DateTime planedReturnDate)
{
    private static int _nextId = 1;

    public int Id { get; } = _nextId++;
    public User User { get; private set; } = user;
    public Equipment Equipment { get; private set; } = equipment;
    public DateTime Date { get; private set; } = date;
    public DateTime PlannedReturnDate { get; private set; } = planedReturnDate;
    
    public DateTime?  ReturnDate { get; private set; }
    public bool IsActive => ReturnDate == null;
    public bool IsOverdue => IsActive 
        ? DateTime.Now > PlannedReturnDate 
        : Date> PlannedReturnDate;    
    
    public void MarkAsReturned(DateTime returnDate)
    {
        if (!IsActive) throw new RentalAvailableException(this.Id);
        
        ReturnDate = returnDate;
    }
}