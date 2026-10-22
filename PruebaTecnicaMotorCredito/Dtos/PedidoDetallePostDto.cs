namespace PruebaTecnicaMotorCredito.Dtos
{
    public class PedidoDetallePostDto
    {
        public int IdProducto { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }
    }
}
