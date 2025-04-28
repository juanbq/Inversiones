using Amazon.DynamoDBv2.DataModel;

namespace DineroAPI.Domain
{
    [DynamoDBTable("Clientes")]
    public class Cliente
    {
        [DynamoDBHashKey("id")]
        public string Id { get; set; }

        [DynamoDBProperty("nombres")]
        public string Nombres { get; set; }
        [DynamoDBProperty("apellidos")]
        public string Apellidos { get; set; }
        [DynamoDBProperty("ciudad")]
        public string Ciudad { get; set; }

    }
}

