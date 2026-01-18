using CarRent.BusinessLogic.Enum;
using CarRent.BusinessLogic.Models;

namespace CarRent.BusinessLogic.Services
{
	public interface IRentalService
	{
		List<Rental> GetExistingRentals();
		Rental RegisterPickup(string bookingNumber, string regNumber, string ssn, CarCategory category, DateTime pickupDateTime, int pickupKm);
		Rental RegisterReturn(string bookingNumber, DateTime returnDateTime, int returnKm, decimal baseDayRental, decimal baseKmPrice);
	}
}
