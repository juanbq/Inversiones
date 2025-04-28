using InversionesWebUI.Model;
using InversionesWebUI.Services;
using Newtonsoft.Json;
using NSubstitute;

namespace InversionesWebUITests
{
    public class InversionesServicesTests
    {
        public HttpClient _client;
        public InversionesService _service;

        //public InversionesServicesTests(HttpClient client) 
        //{

        //    _client = client;
        //}
        [SetUp]
        public void Setup()
        {
            HttpClient client = Substitute.For<HttpClient>();
            
            _service = new InversionesService(client);
        }

        [Test]
        public async Task IsInversionPermitida_RetornaVerdadero_Test()
        {
            var idCliente = 1;
            int valor = 0;
            HttpClient client = Substitute.For<HttpClient>();
            client.BaseAddress = new Uri("http://localhost:5600/api/");
            var respuesta = new HttpResponseMessage
            {
                Content = new StringContent(
                    JsonConvert.SerializeObject(ListaDeProductos())
                )
            };
            client.GetAsync("1").Returns(respuesta);
            var result = await _service.IsInversionPermitida(idCliente, valor);

            Assert.That(result, Is.EqualTo(true));
        }



        [Test]
        public async Task IsInversionPermitida_RetornaFalso_Test()
        {
            var idCliente = 1;
            int valor = 60000;
            HttpClient client = Substitute.For<HttpClient>();
            client.BaseAddress = new Uri("http://localhost:5600/api/");
            client.GetAsync("1").Returns(
                new HttpResponseMessage
                {
                    Content = new StringContent(
                        JsonConvert.SerializeObject(ListaDeProductos())
                    )
                });
            var result = await _service.IsInversionPermitida(idCliente, valor);

            Assert.That(result, Is.False);
        }

        private IEnumerable<Inscripcion> ListaDeProductos()
        {
            var retorno = new List<Inscripcion>();
            retorno.Add(new Inscripcion { IdCliente = 1, IdProducto = 3, Monto = 200000 });
            retorno.Add(new Inscripcion { IdCliente = 1, IdProducto = 3, Monto = 250000 });
            return retorno;
        }
    }
}
