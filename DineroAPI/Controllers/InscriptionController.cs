using Amazon.DynamoDBv2.DataModel;
using DineroAPI.Domain;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DineroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscriptionController(IDynamoDBContext context) : ControllerBase
    {
        private readonly IDynamoDBContext _context = context;

        // GET: api/<InscriptionController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var inscriptions = await _context.ScanAsync<Inscripcion>(default).GetRemainingAsync();
            if (inscriptions == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(inscriptions);
            }
        }
        // GET api/<InscriptionController>/5
        [HttpGet("{idCliente}")]
        public async Task<ActionResult<IEnumerator<Inscripcion>>> GetInscripcionesCliente(int idCliente)
        {
            var inscriptions = await _context.LoadAsync<Inscripcion>(idCliente);
            if (inscriptions == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(inscriptions);
            }
        }

        // POST api/<InscriptionController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Inscripcion inscription)
        {
            if (inscription.Id == string.Empty)
                inscription.Id = Guid.NewGuid().ToString();
            var response= _context.LoadAsync<Inscripcion>(inscription.Id);
            if (response == null || !response.IsFaulted)
            {
                return BadRequest($"Ya existe esa inversion.");
            }
            await _context.SaveAsync<Inscripcion>(inscription);
            return Ok(inscription);
        }

        // DELETE api/<InscriptionController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var nuevaInscripcion = _context.LoadAsync<Inscripcion>(id);
            if (nuevaInscripcion == null)
            {
                return BadRequest($"No existe esa inversion.");
            }

            return Ok();
        }
    }
}
