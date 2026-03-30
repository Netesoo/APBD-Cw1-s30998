namespace RentalApp.Service.Penalty;

public interface IPenaltyCalculator
{
    decimal CalculatePenalty(Models.Rental rental);
}