namespace CarRent.Web.Request
{
	public class ReturnRequest
	{
		public string BookingNumber { get; set; }
		public DateTime ReturnDateTime { get; set; }
		public int ReturnKm { get; set; }
		public decimal BaseDayRental { get; set; }
		public decimal BaseKmPrice { get; set; }
	}
}
