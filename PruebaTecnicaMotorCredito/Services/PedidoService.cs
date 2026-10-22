using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PruebaTecnicaMotorCredito.Data;
using PruebaTecnicaMotorCredito.Dtos;
using PruebaTecnicaMotorCredito.Models;
using PruebaTecnicaMotorCredito.Services.ServiceResponse;

namespace PruebaTecnicaMotorCredito.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;
        public PedidoService(IMapper mapper, DataContext dataContext) 
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }

        public async Task<(Boolean, List<Producto>)> ExisteStock(List<int> IdsProductos)
        {
            var productos = await _dataContext.Productos.Where(c => IdsProductos.Contains(c.Id)).ToListAsync();

            var productosSinStock = productos.Where(c => c.Stock == 0).ToList();

            if (productosSinStock.Count > 0)
            {
                return  (false, productosSinStock);
            }

            return (true, productos);
        }

        async Task<Response<List<ProductoDto>>> IPedidoService.PedidoPostAsync(PedidoPostDto newPedido)
        {
            var transaction = _dataContext.Database.BeginTransaction();

            try
            {
                var pedidoMap = _mapper.Map<Pedido>(newPedido);

                pedidoMap.FechaPedido = DateTime.Now;
                pedidoMap.Estado = "Procesado";
                decimal totalPedido = 0;

                EntityEntry<Pedido> pedidoEnty = _dataContext.Pedidos.Add(pedidoMap);

                await _dataContext.SaveChangesAsync();

                var productosSolicitados = await ExisteStock(newPedido.PedidoDetalle.Select(c => c.IdProducto).ToList());

                if (productosSolicitados.Item1 == false)
                {
                    await transaction.RollbackAsync();
                    return new ServiceResponse.Response<List<ProductoDto>>($"De los productos solicitados, estos no están disponibles. Por favor intente de nuevo", 400, _mapper.Map<List<ProductoDto>>(productosSolicitados.Item2));
                }

                foreach (var detalle in newPedido.PedidoDetalle)
                {
                    var detalleMap = _mapper.Map<PedidoDetalle>(detalle);
                    detalleMap.IdPedido = pedidoEnty.Entity.Id;
                                       
                    _dataContext.PedidoDetalles.Add(detalleMap);

                    var producto = productosSolicitados.Item2.First(c => c.Id == detalle.IdProducto);

                    producto.Stock -= detalle.Cantidad;

                    totalPedido += producto.Precio * detalle.Cantidad;

                    _dataContext.Entry(producto).State = EntityState.Modified;

                }

                pedidoMap.Total = totalPedido;

                await _dataContext.SaveChangesAsync();

                await transaction.CommitAsync();

                return new ServiceResponse.Response<List<ProductoDto>>($"Se ha creado un nuevo pedido correctamente", 200, null);


            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new ServiceResponse.Response<List<ProductoDto>>($"Se ha producido un error {ex.Message}", 400, null);
            }
        }
    }
}
