namespace RentalApp.Exceptions;

public class LimitExceededException(int maxLimit) : Exception($"There is too many active reservations for user {maxLimit}.");