using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaMotorCredito.Dtos;
using PruebaTecnicaMotorCredito.Services;

namespace PruebaTecnicaMotorCredito.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidosController(IPedidoService pedidoService) 
        {
            _pedidoService = pedidoService;
        }

        [HttpPost]
        public async Task<IActionResult> PedidosPostAsync(PedidoPostDto newPedido)
        {
            var result = await _pedidoService.PedidoPostAsync(newPedido);
            if (result.StutusCode == 200)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
