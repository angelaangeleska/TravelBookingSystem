using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBookingSystem.Models
{
    public enum ReservationStatus
    {
        Pending,
        Accepted,
        Rejected
    }

    public class Reservation
    {
        public int Id { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string UserEmail { get; set; }

        [Display(Name = "Full name")]
        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(100, ErrorMessage = "Full name cannot be longer than 100 characters.")]
        public string FullName { get; set; }

        public int PackageId { get; set; }

        [Display(Name = "Number of Adults")]
        [Required(ErrorMessage = "Number of adults is required.")]
        public int NumberOfAdults { get; set; }

        [Display(Name = "Number of Children (4-12 years)")]
        public int NumberOfChildren { get; set; }

        public ReservationStatus Status { get; set; }

        [Display(Name = "Total cost")]
        public decimal TotalCost { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        public virtual Package Package { get; set; }

        [NotMapped]
        public int TotalPeople => NumberOfAdults + NumberOfChildren; // The NotMapped attribute ensures that this property is not mapped to the database.
    }
}
