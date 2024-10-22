using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBookingSystem.Models
{
    public class Destination
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Destination name is required.")]
        [StringLength(100, ErrorMessage = "Destination name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Image URL is required.")]
        [Url(ErrorMessage = "Invalid URL format.")]
        [Display(Name = "Image")]
        public string DestinationImageURL { get; set; }
    }
}
