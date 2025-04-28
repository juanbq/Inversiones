using InversionesWebUI.Model;

namespace InversionesWebUI.Services
{
    public class ProductosService(HttpClient client) : IProductosService
    {
        public async Task<List<Producto>> GetProductos()
        {
            var response = await client.GetAsync("/api/product");
            response.EnsureSuccessStatusCode();
            var products = await response.Content.ReadFromJsonAsync<List<Producto>>();
            return products ?? new List<Producto>();
        }
    }
}
