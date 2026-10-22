using PruebaTecnicaMotorCredito.Models;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaMotorCredito.Dtos
{
    public class PedidoPostDto
    {
        [Required]
        public string NombreCliente { get; set; }

        public List<PedidoDetallePostDto> PedidoDetalle { get; set; }
    }
}
