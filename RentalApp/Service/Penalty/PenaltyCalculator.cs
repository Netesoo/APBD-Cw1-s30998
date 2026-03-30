namespace RentalApp.Service.Penalty;

using RentalApp.Models;

public class PenaltyCalculator : IPenaltyCalculator
{
    private const decimal PenaltyPerDay = 10.0m;

    public decimal CalculatePenalty(Rental rental)
    {
        if (!rental.IsOverdue)
        {
            return 0m; 
        }

        DateTime endDate = rental.ReturnDate ?? DateTime.Now;

        TimeSpan delay = endDate - rental.PlannedReturnDate;

        int daysOverdue = (int)Math.Ceiling(delay.TotalDays);

        if (daysOverdue <= 0)
        {
            return 0m;
        }

        return daysOverdue * PenaltyPerDay;
    }
}