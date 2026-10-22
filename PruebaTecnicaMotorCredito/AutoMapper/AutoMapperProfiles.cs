using AutoMapper;
using PruebaTecnicaMotorCredito.Dtos;
using PruebaTecnicaMotorCredito.Models;

namespace PruebaTecnicaMotorCredito.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<PedidoPostDto, Pedido>();
            CreateMap<PedidoDetallePostDto, PedidoDetalle>();
            CreateMap<Producto, ProductoDto>();
        }

    }
}
