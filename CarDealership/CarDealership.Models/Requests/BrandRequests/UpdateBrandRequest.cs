using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models.Requests.BrandRequests
{
    public class UpdateBrandRequest
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
    }
}
