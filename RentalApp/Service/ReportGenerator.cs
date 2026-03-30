namespace RentalApp.Service;

using RentalApp.Enums;
using System;
using System.Linq;
using System.Text;
using RentalApp.Data;

public class ReportGenerator
{
    private readonly IEquipmentRepository _equipmentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IRentalRepository _rentalRepository;
    
    public ReportGenerator(
        IEquipmentRepository equipmentRepository, 
        IUserRepository userRepository, 
        IRentalRepository rentalRepository)
    {
        _equipmentRepository = equipmentRepository ?? throw new ArgumentNullException(nameof(equipmentRepository));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _rentalRepository = rentalRepository ?? throw new ArgumentNullException(nameof(rentalRepository));
    }
    
    public string GenerateSystemSummary()
    {
        var sb = new StringBuilder();
        
        var allEquipment = _equipmentRepository.GetAll().ToList();
        var allUsers = _userRepository.GetAll().ToList();
        var allRentals = _rentalRepository.GetAll().ToList();
        
        sb.AppendLine("===========================================");
        sb.AppendLine("           RENTAL STATUS REPORT");
        sb.AppendLine("===========================================");
        
        sb.AppendLine("\n--- GENERAL STATISTICS ---");
        sb.AppendLine($"Registered users: {allUsers.Count}");
        sb.AppendLine($"Total equipment count: {allEquipment.Count}");
        sb.AppendLine($"Equipment available immediately: {allEquipment.Count(e => e.Status == RentalStatus.Available)}");
        sb.AppendLine($"Equipment currently rented: {allEquipment.Count(e => e.Status == RentalStatus.Unavailable)}");
        
        sb.AppendLine("\n--- ACTIVE RENTALS ---");
        var activeRentals = allRentals.Where(r => r.IsActive).ToList();
        
        if (!activeRentals.Any())
        {
            sb.AppendLine("No active rentals.");
        }
        else
        {
            foreach (var rental in activeRentals)
            {
                sb.AppendLine($"- {rental.Equipment.Name} rented by: {rental.User.FName} {rental.User.LName}");
                sb.AppendLine($"  Due date: {rental.PlannedReturnDate.ToShortDateString()}");
            }
        }
        sb.AppendLine("===========================================");
        return sb.ToString();
    }
    
    public string GenerateOverdueReport()
    {
        var sb = new StringBuilder();
        
        var overdueRentals = _rentalRepository.GetOverdueRentals().ToList();
        
        sb.AppendLine("===========================================");
        sb.AppendLine("           REPORT: OVERDUE RETURNS");
        sb.AppendLine("===========================================");
        
        if (!overdueRentals.Any())
        {
            sb.AppendLine("Great news: No overdue returns!");
        }
        else
        {
            foreach (var rental in overdueRentals)
            {
                sb.AppendLine($"[URGENT] Equipment: {rental.Equipment.Name}");
                sb.AppendLine($"         Held by: {rental.User.FName} {rental.User.LName}");
                sb.AppendLine($"         Planned return: {rental.PlannedReturnDate.ToShortDateString()}");
                
                var daysLate = (int)Math.Ceiling((DateTime.Now - rental.PlannedReturnDate).TotalDays);
                sb.AppendLine($"         Days late: {daysLate}");
                sb.AppendLine("-------------------------------------------");
            }
        }
        return sb.ToString();
    }
    
    public string GenerateAvailableEquipmentReport()
    {
        var sb = new StringBuilder();
        var availableEquipment = _equipmentRepository.GetAvailableEquipment().ToList();
        
        sb.AppendLine("===========================================");
        sb.AppendLine("           REPORT: AVAILABLE EQUIPMENT");
        sb.AppendLine("===========================================");
        
        if (!availableEquipment.Any())
        {
            sb.AppendLine("No available equipment in stock.");
        }
        else
        {
            foreach (var eq in availableEquipment)
            {
                sb.AppendLine($"- {eq.Name} (ID: {eq.Id})");
            }
        }
        return sb.ToString(); 
    }
}