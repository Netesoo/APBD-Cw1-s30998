namespace RentalApp.Service.Rental;

public interface IRentalService
{
    void RentEquipment(int userId, int equipmentId, int durationInDays);
    decimal ReturnEquipment(int rentalId);
}