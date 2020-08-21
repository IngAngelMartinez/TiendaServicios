using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaServicios.Api.CarritoCompra.Aplicacion;

namespace TiendaServicios.Api.CarritoCompra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoCompras : ControllerBase
    {
        private readonly IMediator mediator;

        public CarritoCompras(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta request) 
        {
            return await mediator.Send(request);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<CarritoDTO>> GetCarrito(int Id) 
        {
            return await mediator.Send(new Consulta.Ejecuta { Id = Id });
        }

    }
}
