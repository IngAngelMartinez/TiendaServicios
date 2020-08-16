using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class ConsultaFiltro
    {
        public class AutorUnico : IRequest<AutorDTO> 
        {
            public string Guid { get; set; }
        }

        public class Manejador : IRequestHandler<AutorUnico, AutorDTO>
        {
            private readonly ContextoAutor contexto;
            private readonly IMapper mapper;

            public Manejador(ContextoAutor contexto, IMapper mapper)
            {
                this.contexto = contexto;
                this.mapper = mapper;
            }

            public async Task<AutorDTO> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                var autor = await contexto.AutorLibro.FirstOrDefaultAsync(T => T.AutorLibroGuid == request.Guid);

                var response = mapper.Map<AutorLibro, AutorDTO>(autor);

                return response;
            }
        }
    }
}
