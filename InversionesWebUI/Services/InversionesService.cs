using InversionesWebUI.Model;

namespace InversionesWebUI.Services
{
    public class InversionesService : IInversionesService
    {
        public readonly HttpClient _client;

        public InversionesService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<Inscripcion>> GetAll()
        {
            try
            {
                var response = await _client.GetAsync("api/inscription");
                response.EnsureSuccessStatusCode();
                var inscripciones = await response.Content.ReadFromJsonAsync<IEnumerable<Inscripcion>>();
                return inscripciones ?? Enumerable.Empty<Inscripcion>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Inscripcion>> GetById(int idCliente, int idProducto)
        {
            try 
            {
                var inscripcion = new Inscripcion { IdProducto = idProducto, IdCliente = idCliente};
                var response = await _client.GetAsync($"api/inscription&idCliente={idCliente}&idProducto={idProducto}");
                response.EnsureSuccessStatusCode();
                var inscripciones = await response.Content.ReadFromJsonAsync<IEnumerable<Inscripcion>>();
                return inscripciones ?? Enumerable.Empty<Inscripcion>();
            } 
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return null;
        }

        public async Task<Inscripcion> Create(Inscripcion inversion)
        {
            var response = await _client.PostAsJsonAsync("api/inscription", inversion);
            
            return inversion;
        }

        public Task Delete(Inscripcion inscripcion)
        {
            try
            {
                var response = _client.DeleteAsync($"api/inscription/{inscripcion.Id}");
            } 
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }

            return Task.CompletedTask;
        }

        public async Task<bool> IsInversionPermitida(int userId, int valorAdicional)
        {
            var response = await _client.GetAsync("api/inscription");
            response.EnsureSuccessStatusCode();
            IEnumerable<Inscripcion> inscripciones = (IEnumerable<Inscripcion>)await response.Content.ReadFromJsonAsync<Inscripcion>();
            if (inscripciones == null)
            {
                return true;
            }
            decimal total = 0;
            foreach (var inscripcion in inscripciones)
            {
                total = total + inscripcion.Monto;
            }
            if (total + valorAdicional > 500000)
            {
                return false;
            }
            return true;
        }
    }
}
