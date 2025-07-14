using Application.MediatR.Vehicles;
using Application.MediatR.Vehicles.CreateVehicle;
using Application.MediatR.Vehicles.GetVehicles;
using Application.MediatR.Vehicles.UpdateVehicle;
using Application.MediatR.VehicleTypes;
using Application.MediatR.VehicleTypes.GetVehicleTypes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace VehicleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VehicleController : ControllerBase
    {
        private readonly IMediator _mediator;
        public VehicleController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [EnableCors("MyPolicy")]
        [ProducesResponseType(typeof(IEnumerable<VehicleDto>), 200)]
        public async Task<IEnumerable<VehicleDto>> GetAllAsync()
        {
            return await _mediator.Send(new GetVehiclesQuery());
        }

        [HttpPost]
        [EnableCors("MyPolicy")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<bool> CreateAsync(CreateVehicleCommand vehicle)
        {
            await _mediator.Send(vehicle);
            return true;
        }

        [HttpPut]
        [EnableCors("MyPolicy")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<bool> UpdateAsync(UpdateVehicleCommand vehicle)
        {
            await _mediator.Send(vehicle);
            return true;
        }      

        [HttpGet("GetVehicleTypes")]
        [EnableCors("MyPolicy")]
        [ProducesResponseType(typeof(IEnumerable<VehicleTypeDto>), 200)]
        public async Task<IEnumerable<VehicleTypeDto>> GetVehicleTypesAsync()
        {
            return await _mediator.Send(new GetVehicleTypesQuery());
        }

        [HttpDelete("{id}")]
        [EnableCors("MyPolicy")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _mediator.Send(new Application.MediatR.Vehicles.DeleteVehicle.DeleteVehicleCommand(id));
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
       
    }
}
