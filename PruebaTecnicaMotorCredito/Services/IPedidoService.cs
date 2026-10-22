
using PruebaTecnicaMotorCredito.Dtos;
using PruebaTecnicaMotorCredito.Models;

namespace PruebaTecnicaMotorCredito.Services
{
    public interface IPedidoService
    {
        Task<ServiceResponse.Response<List<ProductoDto>>> PedidoPostAsync(PedidoPostDto newPedido);

    }
}
