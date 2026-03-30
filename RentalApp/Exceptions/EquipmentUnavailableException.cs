namespace RentalApp.Exceptions;

public class EquipmentUnavailableException(int equipmentId) : Exception($"Equipment id: {equipmentId} is not available.");
