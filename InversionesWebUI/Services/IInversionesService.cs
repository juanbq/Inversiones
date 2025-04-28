namespace InversionesWebUI.Services
{
    public interface IInversionesService
    {
        Task<IEnumerable<InversionesWebUI.Model.Inscripcion>> GetAll();
        Task<InversionesWebUI.Model.Inscripcion> GetById(string id);
        Task<InversionesWebUI.Model.Inscripcion> Create(InversionesWebUI.Model.Inscripcion inversion);
        Task Delete(string id);
        Task<bool> IsInversionPermitida(int userId, int valorAdicional);
    }
}
