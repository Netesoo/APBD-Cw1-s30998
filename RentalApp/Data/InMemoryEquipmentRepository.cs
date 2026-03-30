using RentalApp.Enums;
using RentalApp.Models;

namespace RentalApp.Data;

public class InMemoryEquipmentRepository : IEquipmentRepository
{
    private readonly List<Equipment> _equipmentList = new List<Equipment>();

    public void Add(Equipment equipment)
    {
        if (equipment == null) throw new ArgumentNullException(nameof(equipment));

        if (_equipmentList.Any(e => e.Id == equipment.Id))
        {
            throw new InvalidOperationException("Sprzęt o tym numerze ID już istnieje!");
        }
        
        _equipmentList.Add(equipment);
    }

    public Equipment GetById(int id)
    {
        return _equipmentList.FirstOrDefault(e => e.Id == id);
    }
    
    public IEnumerable<Equipment> GetAll()
    {
        return _equipmentList.AsReadOnly();
    }

    public IEnumerable<Equipment> GetAvailableEquipment()
    {
        return _equipmentList.Where(e => e.Status == RentalStatus.Available).ToList();
    }
}