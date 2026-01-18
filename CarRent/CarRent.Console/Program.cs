using CarRent.BusinessLogic.Enum;
using CarRent.BusinessLogic.Models;
using CarRent.BusinessLogic.Services;
using System.Runtime.Intrinsics.X86;

class Program
{
    static IRentalService rentalService = new RentalService();
    static decimal baseDayRental = 500;
    static decimal baseKmPrice = 2;

    static void Main()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("---------------");
            Console.WriteLine("Current Day Rental : " + baseDayRental);
            Console.WriteLine("Current Km Price : " + baseKmPrice);
            Console.WriteLine("---------------");
            Console.WriteLine("Existing Rentals : ");
            var rentals = rentalService.GetExistingRentals();
            foreach (var item in rentals)
            {
                Console.WriteLine("BookingNo : " + item.BookingNumber + ", RegistrationNumber: " + item.RegistrationNumber + ", SSN: " + item.CustomerSSN + ", Category: " + ((Enum)item.Category).ToString() + ", PickupKm: " + item.PickupKm + ", PickupDate: " + item.PickupDateTime.ToShortDateString());
            }
            Console.WriteLine("---------------");
            Console.WriteLine("Option :");
            Console.WriteLine("1. Register Pickup");
            Console.WriteLine("2. Register Return");
            Console.WriteLine("3. Modify Rental Rates");
            Console.WriteLine("0. Exit");
            Console.Write("Select option: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    RegisterPickup();
                    break;
                case "2":
                    RegisterReturn();
                    break;
                case "3":
                    ModifyRates();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

    static void RegisterPickup()
    {
        var bookingNumber = ReadString("Booking Number: ");
        var regNumber = ReadString("Registration Number: ");
        var ssn = ReadSSN("Customer SSN: ");
        var category = (CarCategory)ReadInt("Car Category (0=Small, 1=Combi, 2=Truck): ");
        var pickupKm = ReadInt("Pickup Km: ");
        var pickupDate = ReadDateTime("Pickup Date (YYYY-MM-DD): ");

        try
        {
            rentalService.RegisterPickup(bookingNumber, regNumber, ssn, category, pickupDate, pickupKm);
            Console.WriteLine("Pickup registered.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error : " + ex.Message);
        }
    }

    static void RegisterReturn()
    {
        var bookingNumber = ReadString("Booking Number: ");
        var returnKm = ReadInt("Return Km: ");
        var returnDate = ReadDateTime("Return Date (YYYY-MM-DD): ");

        try
        {
            var rental = rentalService.RegisterReturn(bookingNumber, returnDate, returnKm, baseDayRental, baseKmPrice);
            Console.WriteLine($"Return registered. Price: {rental.Price}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error : " + ex.Message);
        }
    }

    static void ModifyRates()
    {
        decimal value;
        while (true)
        {
            Console.Write($"Current baseDayRental: {baseDayRental}. Enter new value: ");
            if (decimal.TryParse(Console.ReadLine(), out value))
            {
                baseDayRental = value;
                break;
            }
            Console.WriteLine("Invalid decimal. Please try again.");
        }

        while (true)
        {
            Console.Write($"Current baseKmPrice: {baseKmPrice}. Enter new value: ");
            if (decimal.TryParse(Console.ReadLine(), out value))
            {
                baseKmPrice = value;
                break;
            }
            Console.WriteLine("Invalid decimal. Please try again.");
        }

        Console.WriteLine("Rates updated.");
    }

    static string ReadString(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
            else 
                return input;
        }
    }

    static int ReadInt(string prompt)
    {
        int value;
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out value))
                return value;
            Console.WriteLine("Invalid number. Please try again.");
        }
    }

    static DateTime ReadDateTime(string input)
    {
        DateTime value;
        while (true)
        {
            Console.Write(input);
            if (DateTime.TryParse(Console.ReadLine(), out value))
                return value;
            Console.WriteLine("Invalid date. Please use format YYYY-MM-DD.");
        }
    }

    static string ReadSSN(string prompt)
    {
        int value;
        while (true)
        {
            Console.Write(prompt);
            var ssn = Console.ReadLine();
            if (!ssn.All(char.IsDigit) || ssn.Length != 10)
            {
                Console.WriteLine("invalid ssn, must be 10 digits");
            }
            else
            {
                return ssn;
            }
        }
    }
}
