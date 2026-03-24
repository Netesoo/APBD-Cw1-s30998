using System.Diagnostics.SymbolStore;

namespace RentalApp.Models;

public class Student(string fName, string lName, string major) : User(fName, lName)
{
    public string Major { get; set; } = major;

    public override int GetMaxRentals() => 2;
}