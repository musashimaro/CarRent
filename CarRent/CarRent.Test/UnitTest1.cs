using Xunit;
using CarRent.BusinessLogic.Services;
using CarRent.BusinessLogic.Enum;

namespace CarRent.Test
{
    public class RentalServiceTests
	{
		private decimal _baseDayRental = 500;
		private decimal _baseKmPrice = 2;

		[Fact]
		public void SmallCarPriceCalculation_IsCorrect()
		{
			var service = new RentalService();
			service.RegisterPickup("BOK001", "ABC123", "1234567890", CarCategory.Small, DateTime.Now, 10000);
			var rental = service.RegisterReturn("BOK001", DateTime.Now.AddDays(2), 10200, _baseDayRental, _baseKmPrice);
			Assert.Equal(500 * 2, rental.Price);
		}

		[Fact]
		public void CombiPriceCalculation_IsCorrect()
		{
			var service = new RentalService();
			service.RegisterPickup("BOK002", "DEF456", "0987654321", CarCategory.Combi, DateTime.Now, 10000);
			var rental = service.RegisterReturn("BOK002", DateTime.Now.AddDays(2), 10200, _baseDayRental, _baseKmPrice);
			Assert.Equal(500 * 2 * 1.3m + 2 * 200, rental.Price);
		}

		[Fact]
		public void TruckPriceCalculation_IsCorrect()
		{
			var service = new RentalService();
			service.RegisterPickup("BOK003", "GHI789", "1122334455", CarCategory.Truck, DateTime.Now, 10000);
			var rental = service.RegisterReturn("BOK003", DateTime.Now.AddDays(2), 10200, _baseDayRental, _baseKmPrice);
			Assert.Equal(500 * 2 * 1.5m + 2 * 200 * 1.5m, rental.Price);
		}
	}
}