using AutoMapper;
using MediatR;
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
    public class Consulta
    {
        
        public class ListaAutor : IRequest<List<AutorDTO>> { }

        
        public class Manejador : IRequestHandler<ListaAutor, List<AutorDTO>>
        {
            private readonly ContextoAutor contexto;
            private readonly IMapper mapper;

            public Manejador(ContextoAutor contexto, IMapper mapper)
            {
                this.contexto = contexto;
                this.mapper = mapper;
            }

            public async Task<List<AutorDTO>> Handle(ListaAutor request, CancellationToken cancellationToken)
            {

                var autores = await contexto.AutorLibro.ToListAsync();
                var response = mapper.Map <List<AutorLibro>, List<AutorDTO>>(autores);

                return response;
            }
        }
    }
}
