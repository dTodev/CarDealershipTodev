using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models.Responses.CarResponses
{
    public class DeleteCarResponse : BaseResponse
    {
        public Car Id { get; set; }
    }
}
