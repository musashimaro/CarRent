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
			var rental = new Rental
			{
				BookingNumber = bookingNumber,
				RegistrationNumber = regNumber,
				CustomerSSN = ssn,
				Category = category,
				PickupDateTime = pickupDateTime,
				PickupKm = pickupKm
			};
			_rentals.Add(rental);
			return rental;
		}

		public Rental RegisterReturn(string bookingNumber, DateTime returnDateTime, int returnKm, decimal baseDayRental, decimal baseKmPrice)
		{
			var rental = _rentals.FirstOrDefault(r => r.BookingNumber == bookingNumber);
			if (rental == null) throw new Exception("Rental not found");
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
	}
}