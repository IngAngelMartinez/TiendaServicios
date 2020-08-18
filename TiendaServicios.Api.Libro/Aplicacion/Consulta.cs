using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class Consulta
    {

        public class Ejecuta : IRequest<List<LibroMaterialDTO>> { }

        public class Manejador : IRequestHandler<Ejecuta, List<LibroMaterialDTO>>
        {
            private readonly ContextoLibreria contexto;
            private readonly IMapper mapper;

            public Manejador(ContextoLibreria contexto, IMapper mapper)
            {
                this.contexto = contexto;
                this.mapper = mapper;
            }

            public async Task<List<LibroMaterialDTO>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libros = await contexto.LibreriaMaterial.ToListAsync();

                var response = mapper.Map<List<LibreriaMaterial>, List<LibroMaterialDTO>>(libros);

                return response;

            }
        }
    }
}
