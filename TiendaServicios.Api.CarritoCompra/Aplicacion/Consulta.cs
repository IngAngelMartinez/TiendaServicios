using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.Persistencia;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta : IRequest<CarritoDTO> 
        {
            public int Id { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, CarritoDTO>
        {
            private readonly CarritoContexto contexto;
            private readonly ILibrosService libroService;

            public Manejador(CarritoContexto contexto, ILibrosService libroService)
            {
                this.contexto = contexto;
                this.libroService = libroService;
            }

            public async Task<CarritoDTO> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = await contexto.CarritoSesion.FirstOrDefaultAsync(T => T.Id == request.Id);

                var carritoSesionDetalle = await contexto.CarrioSesionDetalle.Where(T => T.CarritoSesionId == request.Id).ToListAsync();


                var listaCarritoDTO = new List<CarritoDetalleDTO>();


                foreach (var item in carritoSesionDetalle)
                {
                    var response = await libroService.GetLibro(new Guid(item.ProductoSeleccionado));

                    if (response.IsSucced)
                    {
                        var libro = response.Libro;

                        var carritoDetalle = new CarritoDetalleDTO
                        {
                            TituloLibro = libro.Titulo,
                            FechaPublicacion = libro.FechaPublicacion,
                            LibroId = libro.Id,
                            //AutorLibro = libro.AutorLibro,
                        };

                        listaCarritoDTO.Add(carritoDetalle);

                    }
                }


                var carritoSesionDTO = new CarritoDTO()
                {
                    Id = carritoSesion.Id,
                    FechaCreacion = carritoSesion.FechaCreacion,
                    ListaProductos = listaCarritoDTO,
                };

                return carritoSesionDTO;

            }
        }
    }
}
