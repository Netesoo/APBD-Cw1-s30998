namespace RentalApp.Exceptions;

public class EquipmentUnavailableException(int equipmentId) : Exception($"{equipmentId} is not available.");
