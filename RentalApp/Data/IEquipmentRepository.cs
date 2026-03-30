using RentalApp.Models;

namespace RentalApp.Data;

public interface IEquipmentRepository
{
    void Add(Equipment equipment);
    Equipment GetById(int id);
    IEnumerable<Equipment> GetAll();
    IEnumerable<Equipment> GetAvailableEquipment();
}