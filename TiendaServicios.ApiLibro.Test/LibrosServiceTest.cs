using AutoMapper;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using TiendaServicios.Api.Libro.Aplicacion;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;
using TiendaServicios.Api.Libro.Tests;
using Xunit;

namespace TiendaServicios.ApiLibro.Test
{
    
    public class LibrosServiceTest
    {

        private IEnumerable<LibreriaMaterial> ObtenerDataPrueba() 
        {
            A.Configure<LibreriaMaterial>()
             .Fill(T => T.Titulo).AsArticleTitle()
             .Fill(T => T.Id, () => { return Guid.NewGuid(); });

            var lista = A.ListOf<LibreriaMaterial>(30);
            lista[0].Id = Guid.Empty;

            return lista;
        }

        private Mock<ContextoLibreria> CrearContexto() 
        {
            var dataPrueba = ObtenerDataPrueba().AsQueryable();

            var dbSet = new Mock<DbSet<LibreriaMaterial>>();

            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(T => T.Provider).Returns(dataPrueba.Provider);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(T => T.Expression).Returns(dataPrueba.Expression);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(T => T.ElementType).Returns(dataPrueba.ElementType);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(T => T.GetEnumerator()).Returns(dataPrueba.GetEnumerator());

            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(new AsyncQueryProvider<LibreriaMaterial>(dataPrueba.Provider));

            dbSet.As<IAsyncEnumerable<LibreriaMaterial>>()
                 .Setup(T => T.GetAsyncEnumerator(new CancellationToken()))
                 .Returns(new AsyncEnumerator<LibreriaMaterial>(dataPrueba.GetEnumerator()));

            var contexto = new Mock<ContextoLibreria>();
            contexto.Setup(T => T.LibreriaMaterial).Returns(dbSet.Object);

            return contexto;

        }

        [Fact]
        public async void GetLibros() 
        {
            var mockContexto = CrearContexto();

            var mapConfig = new MapperConfiguration(config => config.AddProfile(new MappingTest()));

            var mapper = mapConfig.CreateMapper();

            Consulta.Manejador manejador = new Consulta.Manejador(mockContexto.Object, mapper);

            Consulta.Ejecuta request = new Consulta.Ejecuta();

            var lista = await manejador.Handle(request, new CancellationToken());

            Assert.True(lista.Any());

        }

        [Fact]
        public async void GetLibroPorId()
        {

            var mockContexto = CrearContexto();

            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });

            var mapper = mapConfig.CreateMapper();

            var request = new ConsultaFiltro.Ejecuta();
            request.Id = Guid.Empty;

            var manejador = new ConsultaFiltro.Manejador(mockContexto.Object, mapper);

            var libro = await manejador.Handle(request, new System.Threading.CancellationToken());

            Assert.NotNull(libro);
            Assert.True(libro.Id == Guid.Empty);

        }

        [Fact]
        public async void GuardarLibro()
        {
            System.Diagnostics.Debugger.Launch();


            var options = new DbContextOptionsBuilder<ContextoLibreria>()
                .UseInMemoryDatabase(databaseName: "BaseDatosLibro")
                .Options;

            var contexto = new ContextoLibreria(options);

            var request = new Nuevo.Ejecuta();
            request.Titulo = "Libro de Microservice";
            request.AutorLibro = Guid.Empty;
            request.FechaPublicacion = DateTime.Now;

            var manejador = new Nuevo.Manejador(contexto);

            var libro = await manejador.Handle(request, new System.Threading.CancellationToken());

            Assert.True(libro != null);
        }

    }
}
