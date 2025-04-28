using Amazon.DynamoDBv2.DataModel;

namespace DineroAPI.Domain
{
    [DynamoDBTable("Inscripcion")]
    public class Inscripcion
    {
        [DynamoDBProperty("id")] public string Id { get; set; }
        [DynamoDBHashKey("idCliente")] public int IdCliente { get; set; } 
        [DynamoDBProperty("idProducto")] public int IdProducto { get; set; }

        [DynamoDBProperty("nombreProducto")] public string NombreProducto { get; set; }
        [DynamoDBProperty("nombreDelCliente")] public string NombreDelCliente { get; set; }
        [DynamoDBProperty("monto")] public decimal Monto { get; set; }
        [DynamoDBProperty("tipoProducto")] public string TipoProducto { get; set; }
    }
}
