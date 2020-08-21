using AutoMapper.Configuration.Conventions;
using MediatR;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.Modelo;
using TiendaServicios.Api.CarritoCompra.Persistencia;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest 
        {
            public DateTime? FechaCreacion { get; set; }
            public List<string> ProductoLista { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CarritoContexto contexto;

            public Manejador(CarritoContexto contexto)
            {
                this.contexto = contexto;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {

                var carritoSesion = new CarritoSesion
                {
                    FechaCreacion = request.FechaCreacion
                    
                };

                contexto.CarritoSesion.Add(carritoSesion);
                var value = await contexto.SaveChangesAsync();

                if (value == 0)
                {
                    throw new Exception("Error al agregar al carrito");
                }

                int sesionId = carritoSesion.Id;

                foreach (var item in request.ProductoLista)
                {
                    var detalle = new CarritoSesionDetalle
                    {
                        FechaCreacion = DateTime.Now,
                        CarritoSesionId = sesionId,
                        ProductoSeleccionado = item,
                    };

                    contexto.CarrioSesionDetalle.Add(detalle);
                }

                var valueDetalle = await contexto.SaveChangesAsync();

                if (valueDetalle == 0)
                {
                    throw new Exception("Error al agrear el detalle al carrito de compras");
                }

                return Unit.Value;
            }
        }
    }
}
