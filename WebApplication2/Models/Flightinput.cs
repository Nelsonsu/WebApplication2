using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Flightinput
    {
        public string FromWhere { get; set; }
        public string ToWhere { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string DepartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string ReturnDate { get; set; }
        public string Seatclass { get; set; }
    }
}