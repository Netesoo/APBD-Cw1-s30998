using RentalApp.Enums;

namespace RentalApp.Models;

public abstract class Equipment(string name)
{
    private static int _nextId = 1;
    
    public int Id { get; } = _nextId++;
    public string Name { get; set; } = name;
    public RentalStatus Status { get; set; } = RentalStatus.Available;
}