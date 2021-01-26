using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prueba_newshore.Models
{
    public class Flight
    {
        public int Id { get; set; }

        [Required] 
        [StringLength(150)]
        public string DepartureStation { get; set; }

        [Required]
        [StringLength(150)]
        public string ArrivalStation { get; set; }

        [Required]
        public DateTime DepartureDate { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [StringLength(50)]
        public string Currency { get; set; }

        public int TransportId { get; set; }
        public Transport Transport { get; set; }

    }
}