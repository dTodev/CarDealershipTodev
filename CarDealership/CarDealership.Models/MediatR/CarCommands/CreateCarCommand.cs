using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Models.Requests.CarRequests;
using CarDealership.Models.Responses.CarResponses;
using MediatR;

namespace CarDealership.Models.MediatR.CarCommands
{
    public record CreateCarCommand(CreateCarRequest car) : IRequest<CreateCarResponse>
    {
        public readonly CreateCarRequest _car = car;
    }
}
