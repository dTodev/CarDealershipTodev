using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Models.Requests.CarRequests;
using CarDealership.Models.Responses.CarResponses;
using MediatR;

namespace CarDealership.Models.MediatR.CarCommands
{
    public record UpdateCarCommand(UpdateCarRequest car) : IRequest<UpdateCarResponse>
    {
        public readonly UpdateCarRequest _car = car;
    }
}
