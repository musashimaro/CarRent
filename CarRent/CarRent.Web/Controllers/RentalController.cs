using Microsoft.AspNetCore.Mvc;
using CarRent.BusinessLogic.Services;
using CarRent.Web.Request;

[ApiController]
[Route("[controller]")]
public class RentalController : ControllerBase
{
	private readonly IRentalService _rentalService;

	public RentalController(IRentalService rentalService)
	{
		_rentalService = rentalService;
	}

	[HttpPost("pickup")]
	public IActionResult RegisterPickup([FromBody] PickupRequest request)
	{
		try
		{
			var rental = _rentalService.RegisterPickup(request.BookingNumber, request.RegistrationNumber, request.CustomerSSN, request.Category, request.PickupDateTime, request.PickupKm);
			return Ok(rental);
		}
		catch (Exception)
		{
			return StatusCode(500, new { message = "An unexpected error occurred." });
		}
	}

	[HttpPost("return")]
	public IActionResult RegisterReturn([FromBody] ReturnRequest request)
	{
		try
		{
			var rental = _rentalService.RegisterReturn(request.BookingNumber, request.ReturnDateTime, request.ReturnKm, request.BaseDayRental, request.BaseKmPrice);
			return Ok(rental);
		}
		catch (Exception ex)
		{
			if (ex.Message == "Rental not found")
				return NotFound(new { message = ex.Message });
			return StatusCode(500, new { message = "An unexpected error occurred." });
		}
	}
}