using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models
{
    public class Purchase
    {
        public Guid Id { get; set; }
        public int CarId { get; set; }
        public int ClientId { get; set; }
        public decimal Price { get; set; }
    }
}
