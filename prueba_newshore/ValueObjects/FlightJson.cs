using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prueba_newshore.ValueObjects
{
    public class FlightJson
    {     
               

        public string DepartureStation { get; set; }
        
        public string ArrivalStation { get; set; }
       
        public DateTime DepartureDate { get; set; }
        
        public decimal Price { get; set; }
        
        public string Currency { get; set; }
        
        public string FlightNumber { get; set; }


    }
}