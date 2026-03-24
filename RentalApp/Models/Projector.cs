namespace RentalApp.Models;

public class Projector(string name, string resolution, int lumens) : Equipment(name)
{
    public string Resolution { get; set; } = resolution;
    public int Lumens { get; set; } = lumens;
}