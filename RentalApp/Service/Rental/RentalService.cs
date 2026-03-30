using System.ComponentModel;
using RentalApp.Data;
using RentalApp.Enums;
using RentalApp.Exceptions;
using RentalApp.Service.Penalty;

namespace RentalApp.Service.Rental;

public class RentalService : IRentalService
{
    private readonly IUserRepository _userRepository;
    private readonly IRentalRepository _rentalRepository;
    private readonly IEquipmentRepository _equipmentRepository;
    private readonly IPenaltyCalculator _penaltyCalculator;
    
    public RentalService(
        IUserRepository userRepository,
        IRentalRepository rentalRepository,
        IEquipmentRepository equipmentRepository,
        IPenaltyCalculator penaltyCalculator)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _equipmentRepository = equipmentRepository ?? throw new ArgumentNullException(nameof(equipmentRepository));
        _rentalRepository = rentalRepository ?? throw new ArgumentNullException(nameof(rentalRepository));
        _penaltyCalculator = penaltyCalculator ?? throw new ArgumentNullException(nameof(penaltyCalculator));
    }

    public void RentEquipment(int userId, int equipmentId, int durationInDays)
    {
        var user = _userRepository.GetById(userId);
        if (user == null) throw new UserNotFoundException(userId);

        var equipment = _equipmentRepository.GetById(equipmentId);
        if (equipment == null) throw new EquipmentNotFoundException(equipmentId);

        if (equipment.Status != RentalStatus.Available) throw new EquipmentUnavailableException(equipmentId);

        var activeRentals = _rentalRepository.GetActiveRentalsForUser(userId);

        if (activeRentals.Count() >= user.GetMaxRentals()) throw new LimitExceededException(userId);

        equipment.Status = RentalStatus.Unavailable;

        var rental = new Models.Rental(
            user,
            equipment,
            DateTime.Now,
            DateTime.Now.AddDays(durationInDays)
        );

        _rentalRepository.Add(rental);
    }

    public decimal ReturnEquipment(int rentalId)
    {
        var rental = _rentalRepository.GetById(rentalId);
        if (rental == null) throw new ReturnNotFoundException(rentalId);
        if (rental.Equipment.Status == RentalStatus.Available) throw new RentalAvailableException(rentalId);
        
        rental.MarkAsReturned(DateTime.Now);
        rental.Equipment.Status = RentalStatus.Available;
        
        return _penaltyCalculator.CalculatePenalty(rental);
    }
}
    
