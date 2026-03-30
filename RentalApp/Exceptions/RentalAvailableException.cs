namespace RentalApp.Exceptions;

public class RentalAvailableException(int Id) : Exception($"Rental with Id: {Id} has already been returned!");