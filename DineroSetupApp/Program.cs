using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using DineroAPI.Domain;

internal class Program
{
    public static IAmazonDynamoDB Client = new AmazonDynamoDBClient();
    public const string ProductTableName = "Producto";

    static async Task Main(string[] args)
    {
        Console.WriteLine("\n **Creando tablas de la applicacion**\n");
        await CreateExampleTable();
    }

    public static async Task CreateExampleTable()
    {
        //await CreaTablaProductos();
        await CreateTablaInscripcion();
        //await CreateTablaClientes();
    }

    private static async Task CreateTablaInscripcion()
    {
        try
        {
            var response = await Client.CreateTableAsync(new CreateTableRequest
            {
                TableName = "Inscripcion",
                AttributeDefinitions = new List<AttributeDefinition>
                {
                    new AttributeDefinition
                    {
                        AttributeName = "idCliente",
                        AttributeType = ScalarAttributeType.N
                    }
                    //,
                    //new AttributeDefinition
                    //{
                    //    AttributeName = "idProducto",
                    //    AttributeType = ScalarAttributeType.N
                    //}
                },
                KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement
                    {
                        AttributeName = "idCliente",
                        KeyType = KeyType.HASH
                    }
                },

                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 10,
                    WriteCapacityUnits = 5
                }
            });

            await WaitUntilTableReady(ProductTableName);
        }
        catch (ResourceInUseException e)
        {
            Console.WriteLine("Ya existe la tabla");
            //throw;
        }

        Console.WriteLine("Tabla creada correctamente");
        await PutItemsProductos();
    }

    private static async Task CreateTablaClientes()
    {
        try
        {
            var response = await Client.CreateTableAsync(new CreateTableRequest
            {
                TableName = "Clientes",
                AttributeDefinitions = new List<AttributeDefinition>
                {
                    new AttributeDefinition
                    {
                        AttributeName = "idCliente",
                        AttributeType = ScalarAttributeType.N
                    }
                },
                KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement
                    {
                        AttributeName = "idCliente",
                        KeyType = KeyType.HASH
                    }
                },

                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 10,
                    WriteCapacityUnits = 5
                }
            });

            await WaitUntilTableReady(ProductTableName);
        }
        catch (ResourceInUseException e)
        {
            Console.WriteLine("Ya existe la tabla");
            //throw;
        }

        Console.WriteLine("Tabla creada correctamente");
        await PutItemsProductos();
    }

    private static async Task CreaTablaProductos()
    {
        try
        {
            var response = await Client.CreateTableAsync(new CreateTableRequest
            {
                TableName = ProductTableName,
                AttributeDefinitions = new List<AttributeDefinition>
                {
                    new AttributeDefinition
                    {
                        AttributeName = "Id",
                        AttributeType = ScalarAttributeType.N
                    }
                },
                KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement
                    {
                        AttributeName = "Id",
                        KeyType = KeyType.HASH
                    }
                },

                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 10,
                    WriteCapacityUnits = 5
                }
            });

            await WaitUntilTableReady(ProductTableName);
        }
        catch (ResourceInUseException e)
        {
            Console.WriteLine("Ya existe la tabla");
            await DeleteTablaProductos();
            //throw;
        }

        Console.WriteLine("Tabla creada correctamente");
        await PutItemsProductos();
        return;
    }

    public static async Task WaitUntilTableReady(string tableName)
    {
        string? status = null;
        do
        {
            Thread.Sleep(5000);
            try
            {
                var response = await Client.DescribeTableAsync(new DescribeTableRequest
                {
                    TableName = tableName
                });

                Console.WriteLine($"Tabla : {response.Table.TableName}, estado: {response.Table.TableStatus}");
                status = response.Table.TableStatus;
            }
            catch (ResourceNotFoundException)
            {
                //La tabla sera creada eventualmente
            }
        } while (status != "ACTIVE");
    }

    public static async Task DeleteTablaProductos()
    {
        var response = await Client.DeleteTableAsync(new DeleteTableRequest
        {
            TableName = ProductTableName
        });
    }
    public static async Task PutItemsProductos()
    {
        var product = new Producto
        {
            Id = 1,
            Nombre = "FPV_EL_CLIENTE_RECAUDADORA",
            TipoProducto = "FPV",
            ValorProducto = 75000
        };
        await PutItemAsync("Producto", product);


        product = new Producto
         {
            Id = 2,
            Nombre = "FPV_EL CLIENTE_ECOPETROL",
            TipoProducto = "FPV",
            ValorProducto = 125000
        };
        await PutItemAsync("Producto", product);

         product = new Producto
        {
            Id = 3,
            Nombre = "DEUDAPRIVADA",
            TipoProducto = "FIC",
            ValorProducto = 50000
        };
        await PutItemAsync("Producto", product);

        product = new Producto
        {
            Id = 4,
            Nombre = "FDO-ACCIONES",
            TipoProducto = "FIC",
            ValorProducto = 50000
        };
        await PutItemAsync("Producto", product);

        product = new Producto
        {
            Id = 5,
            Nombre = "FDO-ACCIONES",
            TipoProducto = "FIC",
            ValorProducto = 250000
        };
        await PutItemAsync("Producto", product);

        product = new Producto
        {
            Id = 5,
            Nombre = "FPV_EL CLIENTE_DINAMICA",
            TipoProducto = "FPV",
            ValorProducto = 100000
        };
        await PutItemAsync("Producto", product);

    }

    public static async Task PutItemAsync<T>(string tableName, T item) where T : class
    {
        var itemDictionary = new Dictionary<string, AttributeValue>();

        foreach (var property in typeof(T).GetProperties())
        {
            var value = property.GetValue(item);
            if (value != null)
            {
                if (value is string)
                {
                    itemDictionary[property.Name] = new AttributeValue { S = value.ToString() };
                }
                else if (value is int || value is long || value is double)
                {
                    itemDictionary[property.Name] = new AttributeValue { N = value.ToString() };
                }
            }
        }

        var request = new PutItemRequest
        {
            TableName = tableName,
            Item = itemDictionary
        };

        await Client.PutItemAsync(request);
    }

}
