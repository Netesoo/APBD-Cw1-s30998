namespace RentalApp.Models;

public class Laptop(string name, string processor, int ramInGb, double sizeInInch, double weight) : Equipment(name)
{
    public string Processor { get; set; } = processor;
    public double SizeInInch { get; } = sizeInInch;
    public double Weight { get; } = weight;
}