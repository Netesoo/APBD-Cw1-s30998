namespace RentalApp.Models;

public abstract class User(string fName, string lName)
{
    private static int _nextId = 1;

    public int Id { get; set; } = _nextId++;
    public string FName { get; set; } = fName;
    public string LName { get; set; } = lName;

    public abstract int GetMaxRentals();
}