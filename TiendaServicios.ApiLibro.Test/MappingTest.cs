using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TiendaServicios.Api.Libro.Modelo;

namespace TiendaServicios.ApiLibro.Test
{
    public class MappingTest : Profile
    {

        public MappingTest()
        {
            CreateMap<LibreriaMaterial, LibroMaterialDTO>();
        }

    }
}
