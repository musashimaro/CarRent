using CarRent.BusinessLogic.Enum;

namespace CarRent.BusinessLogic.Models
{
	public class Rental
	{
		public string BookingNumber { get; set; }
		public string RegistrationNumber { get; set; }
		public string CustomerSSN { get; set; }
		public CarCategory Category { get; set; }
		public DateTime PickupDateTime { get; set; }
		public int PickupKm { get; set; }
		public DateTime? ReturnDateTime { get; set; }
		public int? ReturnKm { get; set; }
		public decimal? Price { get; set; }

		public int MaxReturnDays { get; set; }


	}
}
