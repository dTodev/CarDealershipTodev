using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models.Responses.BrandResponses
{
    public class CreateBrandResponse : BaseResponse
    {
        public Brand BrandName { get; set; }
    }
}
