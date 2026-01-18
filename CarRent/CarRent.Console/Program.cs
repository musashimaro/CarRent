using CarRent.BusinessLogic.Enum;
using CarRent.BusinessLogic.Services;

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
				Console.WriteLine(item.BookingNumber + "," + item.Category + "," + item.CustomerSSN + "," + ((Enum)item.Category).ToString() + "," + item.PickupKm + "," + item.PickupDateTime);
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
		Console.Write("Booking Number: ");
		var bookingNumber = Console.ReadLine();
		Console.Write("Registration Number: ");
		var regNumber = Console.ReadLine();
		Console.Write("Customer SSN: ");
		var ssn = Console.ReadLine();
		Console.WriteLine("Car Category (0=Small, 1=Combi, 2=Truck): ");
		var category = (CarCategory)int.Parse(Console.ReadLine() ?? "0");
		var pickupKm = ReadInt("Pickup Km: ");        
        var pickupDate = ReadDateTime("Pickup Date (YYYY-MM-DD): ");

        rentalService.RegisterPickup(bookingNumber, regNumber, ssn, category, pickupDate, pickupKm);
		Console.WriteLine("Pickup registered.");
	}

	static void RegisterReturn()
	{
		Console.Write("Booking Number: ");
		var bookingNumber = Console.ReadLine();
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
		Console.Write($"Current baseDayRental: {baseDayRental}. Enter new value: ");
		baseDayRental = decimal.Parse(Console.ReadLine() ?? baseDayRental.ToString());
		Console.Write($"Current baseKmPrice: {baseKmPrice}. Enter new value: ");
		baseKmPrice = decimal.Parse(Console.ReadLine() ?? baseKmPrice.ToString());
		Console.WriteLine("Rates updated.");
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

    static decimal ReadDecimal(string prompt)
    {
        decimal value;
        while (true)
        {
            Console.Write(prompt);
            if (decimal.TryParse(Console.ReadLine(), out value))
                return value;
            Console.WriteLine("Invalid decimal. Please try again.");
        }
    }

    static DateTime ReadDateTime(string prompt)
    {
        DateTime value;
        while (true)
        {
            Console.Write(prompt);
            if (DateTime.TryParse(Console.ReadLine(), out value))
                return value;
            Console.WriteLine("Invalid date. Please use format YYYY-MM-DD.");
        }
    }
}
