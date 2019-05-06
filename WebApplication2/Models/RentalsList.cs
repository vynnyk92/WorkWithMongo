using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Rentals;

namespace WebApplication2.Models
{
    public class RentalsList
    {
        public IEnumerable<Rental> Rentals { get; set; }
        public RentalsFilter RentalsFilter { get; set; }
    }
}