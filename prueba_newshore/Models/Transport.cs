using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prueba_newshore.Models
{
    public class Transport
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string FlightNumber { get; set; }

        


    }
}