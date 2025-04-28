using InversionesWebUI.Model;

namespace InversionesWebUI.Services
{
    public interface IProductosService
    {
        Task<List<Producto>> GetProductos();
    }
}
