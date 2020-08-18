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
    public class ConsultaFiltro
    {
        public class Ejecuta : IRequest<LibroMaterialDTO>
        {
            public Guid Id { get; set; }
        }


        public class Manejador : IRequestHandler<Ejecuta, LibroMaterialDTO>
        {
            private readonly ContextoLibreria contexto;
            private readonly IMapper mapper;

            public Manejador(ContextoLibreria contexto, IMapper mapper)
            {
                this.contexto = contexto;
                this.mapper = mapper;
            }

            public async Task<LibroMaterialDTO> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libro = await contexto.LibreriaMaterial.Where(T => T.Id == request.Id).FirstOrDefaultAsync();

                if (libro == null)
                {
                    throw new Exception("No se encontro el libro");
                }

                var response = mapper.Map<LibreriaMaterial, LibroMaterialDTO>(libro);

                return response;


            }
        }

    }
}
