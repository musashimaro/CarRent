using CarRent.BusinessLogic.Enum;

namespace CarRent.Web.Request
{	public class PickupRequest
	{
		public string BookingNumber { get; set; }
		public string RegistrationNumber { get; set; }
		public string CustomerSSN { get; set; }
		public CarCategory Category { get; set; }
		public DateTime PickupDateTime { get; set; }
		public int PickupKm { get; set; }
	}
}
