namespace InversionesWebUI.Model
{
    public class Inscripcion
    {
        public string Id { get; set; }
        public int IdCliente { get; set; }
        public int IdProducto { get; set; }
        public string TipoProducto { get; set;}
        public string NombreProducto { get; set; }
        public string NombreDelCliente { get; set; }
        public decimal Monto { get; set; }
    }
}
