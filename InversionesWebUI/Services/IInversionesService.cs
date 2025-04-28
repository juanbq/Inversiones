using InversionesWebUI.Model;

namespace InversionesWebUI.Services
{
    public interface IInversionesService
    {
        Task<IEnumerable<InversionesWebUI.Model.Inscripcion>> GetAll();
        //Task<InversionesWebUI.Model.Inscripcion> GetById(string id);
        Task<IEnumerable<InversionesWebUI.Model.Inscripcion>> GetById(int idCliente, int idProducto);
        Task<InversionesWebUI.Model.Inscripcion> Create(InversionesWebUI.Model.Inscripcion inversion);
        Task Delete(Inscripcion inscripcion);
        Task<bool> IsInversionPermitida(int idCliente, int valorAdicional);
    }
}
