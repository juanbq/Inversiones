using Amazon.DynamoDBv2.DataModel;

namespace DineroAPI.Domain
{
    public class Producto
    {
        [DynamoDBHashKey("Id")]
        public int Id { get; set; }
        [DynamoDBProperty("Nombre")]
        public string Nombre { get; set; }
        [DynamoDBProperty("TipoProducto")]
        public string TipoProducto { get; set; }
        [DynamoDBProperty("ValorProducto")]
        public int ValorProducto { get; set; }
    }
}
