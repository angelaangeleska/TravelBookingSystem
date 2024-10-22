using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelBookingSystem.Models
{
    public class Package
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int DestinationId { get; set; }
        public virtual Destination Destination { get; set; }
        [Required]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Invalid price format. Use a number with up to two decimal places.")]
        public decimal Price { get; set; }
        [Required]
        [Display(Name = "Departure date")]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "Return date")]
        public DateTime EndDate { get; set; }
        [Required]
        public int AccommodationId { get; set; }
        public virtual Accommodation Accommodation { get; set; }
        [Required]
        [Display(Name = "Maximum capacity")]
        [Range(0, int.MaxValue, ErrorMessage = "Available places must be a positive number.")]

        public int AvailablePlaces { get; set; }
    }
}