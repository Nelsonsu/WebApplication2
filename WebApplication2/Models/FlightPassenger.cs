using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class FlightPassenger
    {
        [Required(ErrorMessage = "Passport is required")]
        public string Passport { get; set; }

        [Required(ErrorMessage = "Passenger first name is required")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Passenger last name is required")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Passport is required")]
        [Range(1, 99, ErrorMessage = "Please enter valid integer Number")]
        public int Age { get; set; }
        public string Gender { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        public string Flightsid1 { get; set; }
        public string Flightid2 { get; set; }
    }
}