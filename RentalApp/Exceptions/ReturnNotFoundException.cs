namespace RentalApp.Exceptions;

public class ReturnNotFoundException(int Id) : Exception($"Rental  with Id: {Id} does not exist.");