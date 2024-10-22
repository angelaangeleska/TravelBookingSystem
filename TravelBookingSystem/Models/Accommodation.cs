using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelBookingSystem.Models
{
    public class Accommodation
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Accommodation name is required.")]
        [StringLength(100, ErrorMessage = "Accommodation name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Image URL is required.")]
        [Url(ErrorMessage = "Invalid URL format.")]
        [Display(Name = "Image")]
        public string AccommodationImageURL { get; set; }
    }
}
