namespace RentalApp.Models;

public class Employee(string fName, string lName, string department) : User(fName, lName)
{
    public string Department { get; set; } = department;

    public override int GetMaxRentals() => 5;
}