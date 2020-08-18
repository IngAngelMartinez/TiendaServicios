using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest 
        {

            public string Titulo { get; set; }
            public DateTime? FechaPublicacion { get; set; }
            public Guid? AutorLibro { get; set; }
        }

        public class Validacion : AbstractValidator<Ejecuta> 
        {
            public Validacion()
            {
                RuleFor(T => T.Titulo).NotEmpty();
                RuleFor(T => T.FechaPublicacion).NotEmpty();
                RuleFor(T => T.AutorLibro).NotEmpty();

            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ContextoLibreria contexto;

            public Manejador(ContextoLibreria contexto)
            {
                this.contexto = contexto;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libro = new LibreriaMaterial
                {
                    Titulo = request.Titulo,
                    FechaPublicacion = request.FechaPublicacion,
                    AutorLibro = request.AutorLibro,
                };

                contexto.LibreriaMaterial.Add(libro);

                var response = await contexto.SaveChangesAsync();

                if (response > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo guardar el libro");
            }
        }
    }
}
