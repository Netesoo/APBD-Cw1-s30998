namespace RentalApp.Models;

public class Camera(string name, string resolution, int memoryInGb) : Equipment(name)
{
    public string Resolution { get; set; } = resolution;
    public int MemoryInGb { get; set; } = memoryInGb;
}