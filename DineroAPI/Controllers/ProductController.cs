using Amazon.DynamoDBv2.DataModel;
using DineroAPI.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DineroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IDynamoDBContext context) : ControllerBase
    {
        private readonly IDynamoDBContext _context = context;

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> Get()
        {
            var products = await _context.ScanAsync<Producto>(new List<ScanCondition>()).GetRemainingAsync();
            return Ok(products);
        }
    }
}
