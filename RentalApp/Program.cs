using System;
using System.Linq;
using RentalApp.Data;
using RentalApp.Enums;
using RentalApp.Models;
using RentalApp.Service;
using RentalApp.Service.Rental;
using RentalApp.Service.Penalty;

namespace RentalApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // System initialization (Composition Root)
            IEquipmentRepository equipmentRepo = new InMemoryEquipmentRepository();
            IUserRepository userRepo = new InMemoryUserRepository();
            IRentalRepository rentalRepo = new InMemoryRentalRepository();
            IPenaltyCalculator penaltyCalculator = new PenaltyCalculator();

            IRentalService rentalService = new RentalService(
                userRepo, rentalRepo, equipmentRepo, penaltyCalculator);
            
            var reportGenerator = new ReportGenerator(equipmentRepo, userRepo, rentalRepo);

            // Loading initial data
            var laptop = new Laptop("Dell XPS 15", "Intel Core i7", 16, 15.6, 2.2);
            var projector = new Projector("Epson 4K", "4K UHD", 3000);
            var camera = new Camera("Sony A7 III", "6000 x 4000", 128);
            
            equipmentRepo.Add(laptop);
            equipmentRepo.Add(projector);
            equipmentRepo.Add(camera);

            var student = new Student("Jan", "Kowalski", "Computer Science");
            var employee = new Employee("Anna", "Nowak", "Physics Department");

            userRepo.Add(student);
            userRepo.Add(employee);

            // Successful equipment rental
            try
            {
                rentalService.RentEquipment(student.Id, laptop.Id, 3);
                Console.WriteLine($"{student.FName} rented {laptop.Name}.");
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message); 
            }
            
            // Attempt to rent unavailable equipment
            try
            {
                rentalService.RentEquipment(employee.Id, laptop.Id, 2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Attempt to exceed the rental limit by a student
            try
            {
                rentalService.RentEquipment(student.Id, projector.Id, 1);
                Console.WriteLine($"{student.FName} rented {projector.Name}.");
                
                rentalService.RentEquipment(student.Id, camera.Id, 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Returning equipment on time
            try
            {
                var activeRental = rentalRepo.GetActiveRentalsForUser(student.Id)
                                             .First(r => r.Equipment.Id == projector.Id);

                decimal penalty = rentalService.ReturnEquipment(activeRental.Id);
                Console.WriteLine($"Returned {projector.Name}. Applied penalties: {penalty} PLN.");
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message); 
            }

            // Returning equipment late
            try
            {
                rentalService.RentEquipment(employee.Id, camera.Id, -5);
                var lateRental = rentalRepo.GetActiveRentalsForUser(employee.Id).First();

                Console.WriteLine($"\nEmployee returns {camera.Name} with a significant delay...");
                decimal penalty = rentalService.ReturnEquipment(lateRental.Id);
                Console.WriteLine($"Returned {camera.Name}. Applied penalties: {penalty} PLN.");
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message); 
            }

            Console.WriteLine("\nRENTAL HISTORY:");
            foreach (var r in rentalRepo.GetAll())
            {
                string returnStatus = r.IsActive ? "In progress" : $"Returned (Date: {r.ReturnDate?.ToShortDateString()})";
                Console.WriteLine($"- {r.User.FName} {r.User.LName} -> {r.Equipment.Name} | Status: {returnStatus}");
            }
            
            Console.WriteLine(reportGenerator.GenerateSystemSummary());
            Console.WriteLine(reportGenerator.GenerateOverdueReport());
            Console.WriteLine(reportGenerator.GenerateAvailableEquipmentReport());
        }
    }
}