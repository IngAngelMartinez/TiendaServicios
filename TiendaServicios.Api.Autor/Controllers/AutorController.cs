using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using TiendaServicios.Api.Autor.Aplicacion;
using TiendaServicios.Api.Autor.Modelo;

namespace TiendaServicios.Api.Autor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IMediator mediator;

        public AutorController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data) 
        {
            return await mediator.Send(data);
        }


        [HttpGet]
        public async Task<ActionResult<List<AutorDTO>>> Get() 
        {
            return await mediator.Send(new Consulta.ListaAutor());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AutorDTO>> Get(string id)
        {
            return await mediator.Send(new ConsultaFiltro.AutorUnico { Guid = id });
        }

    }
}
