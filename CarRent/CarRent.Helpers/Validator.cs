using CarRent.BusinessLogic.Enum;
using CarRent.BusinessLogic.Models;

namespace CarRent.Helpers
{
    public static class Validator
    {
        public static void ValidateInputPickup(string bookingNumber, string regNumber, string ssn, CarCategory category, DateTime pickupDateTime, int pickupKm)
        {
            if (string.IsNullOrWhiteSpace(bookingNumber)) throw new Exception("invalid booking number");
            if (string.IsNullOrWhiteSpace(regNumber)) throw new Exception("invalid registration number");
            int value;
            if (int.TryParse(ssn, out value)) throw new Exception("invalid ssn, must be number");
            if (value.ToString().Length != 10) throw new Exception("invalid ssn, must be 10 digit");
            if (pickupKm < 0) throw new Exception("invalid pickupKm, must greater than 0");
        }

        public static void ValidateInputReturn(string bookingNumber, DateTime returnDateTime, int returnKm, decimal baseDayRental, decimal baseKmPrice)
        {
            if (string.IsNullOrWhiteSpace(bookingNumber)) throw new Exception("invalid booking number");
            if (returnKm < 0) throw new Exception("invalid returnKm, must greater than 0");
            if (baseDayRental < 0) throw new Exception("invalid baseDayRental, must greater than 0");
            if (baseKmPrice < 0) throw new Exception("invalid baseKmPrice, must greater than 0");

            if (rental == null) throw new Exception("Rental not found");
            if (rental.PickupDateTime < returnDateTime) throw new Exception("wrong input: return date later than pickup date");
            if (rental.PickupKm > returnKm) throw new Exception("wrong input: return km smaller than pickup km");


            return rental;
        }
    }
}
