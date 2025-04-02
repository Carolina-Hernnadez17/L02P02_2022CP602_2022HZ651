}namespace L02P02_2022CP602_2022HZ651.Models
{
    public class CierreVentaViewModel
    {
       
        public int PedidoId { get; set; }
        public string ClienteNombre { get; set; }
        public string ClienteApellido { get; set; }
        public string ClienteEmail { get; set; }
        public string ClienteDireccion { get; set; }
        public List<DetallePedidoViewModel> Detalles { get; set; }
        public decimal Total { get; set; }
    }

    public class DetallePedidoViewModel
    {
        public string LibroNombre { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}
