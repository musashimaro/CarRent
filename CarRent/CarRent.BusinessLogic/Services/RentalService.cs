using CarRent.BusinessLogic.Enum;
using CarRent.BusinessLogic.Models;

namespace CarRent.BusinessLogic.Services
{
    public class RentalService : IRentalService
	{
		private readonly List<Rental> _rentals = new();

		public List<Rental>GetExistingRentals()
		{
			return _rentals.ToList();
		}

		public Rental RegisterPickup(string bookingNumber, string regNumber, string ssn, CarCategory category, DateTime pickupDateTime, int pickupKm)
		{
            var rental = ValidateInputPickup(bookingNumber, regNumber, ssn, category, pickupDateTime, pickupKm);
			_rentals.Add(rental);
			return rental;
		}

		public Rental RegisterReturn(string bookingNumber, DateTime returnDateTime, int returnKm, decimal baseDayRental, decimal baseKmPrice)
		{
            var rental = ValidateInputReturn(bookingNumber, returnDateTime, returnKm, baseDayRental, baseKmPrice);
            rental.ReturnDateTime = returnDateTime;
			rental.ReturnKm = returnKm;
			rental.Price = CalculatePrice(rental, baseDayRental, baseKmPrice);
            _rentals.Remove(rental);
            return rental;
		}

		private decimal CalculatePrice(Rental rental, decimal baseDayRental, decimal baseKmPrice)
		{
			int days = (int)Math.Ceiling((rental.ReturnDateTime?.Date - rental.PickupDateTime.Date).Value.TotalDays);
			int km = rental.ReturnKm.Value - rental.PickupKm;
			return rental.Category switch
			{
				CarCategory.Small => baseDayRental * days,
				CarCategory.Combi => baseDayRental * days * 1.3m + baseKmPrice * km,
				CarCategory.Truck => baseDayRental * days * 1.5m + baseKmPrice * km * 1.5m,
				_ => throw new NotImplementedException()
			};
		}

        private Rental ValidateInputPickup(string bookingNumber, string regNumber, string ssn, CarCategory category, DateTime pickupDateTime, int pickupKm)
        {
			if (string.IsNullOrWhiteSpace(bookingNumber)) throw new Exception("invalid booking number");
            if (string.IsNullOrWhiteSpace(regNumber)) throw new Exception("invalid registration number");            
            if (!ssn.All(char.IsDigit) || ssn.Length != 10) throw new Exception("invalid ssn, must be 10 digits");
            if (!System.Enum.IsDefined(typeof(CarCategory), category)) throw new Exception("invalid category");
            if (pickupKm < 0) throw new Exception("invalid pickupKm, must greater than 0");

			return new Rental
            {
                BookingNumber = bookingNumber,
                RegistrationNumber = regNumber,
                CustomerSSN = ssn,
                Category = category,
                PickupDateTime = pickupDateTime,
                PickupKm = pickupKm
            };
        }
        private Rental ValidateInputReturn(string bookingNumber, DateTime returnDateTime, int returnKm, decimal baseDayRental, decimal baseKmPrice)
		{
            if (string.IsNullOrWhiteSpace(bookingNumber)) throw new Exception("invalid booking number");
            if (returnKm < 0) throw new Exception("invalid returnKm, must greater than 0");
            if (baseDayRental < 0) throw new Exception("invalid baseDayRental, must greater than 0");
            if (baseKmPrice < 0) throw new Exception("invalid baseKmPrice, must greater than 0");

            var rental = _rentals.FirstOrDefault(r => r.BookingNumber == bookingNumber);
			if (rental == null) throw new Exception("Rental not found");
            if (rental.PickupDateTime > returnDateTime) throw new Exception("wrong input: return date later than pickup date");
            if (rental.PickupKm > returnKm) throw new Exception("wrong input: return km smaller than pickup km");

            return rental;
        }
	}
}