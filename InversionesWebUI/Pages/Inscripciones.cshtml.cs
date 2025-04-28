using InversionesWebUI.Model;
using InversionesWebUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InversionesWebUI.Pages
{
    
    public class InscripcionesModel(IInversionesService service) : PageModel
    {
        public Inscripcion[]? inscripciones { get; set; }

        private readonly IInversionesService _service = service;
        public int IdProductoSeleccionado { get; set; }

        public async Task OnGetAsync()
        {
            var response = await _service.GetAll();
            inscripciones = response.ToArray();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var inversionCliente = new Inscripcion
            {
                IdProducto = IdProductoSeleccionado,
                IdCliente = 102025
            };
            var response = await _service.Create(inversionCliente);
            return Page();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int idCliente,int idProducto)
        {
            var producto = _service.GetById(idCliente, idProducto).Result.FirstOrDefault();

            if (producto == null) return NotFound();

            await _service.Delete(producto);

            return RedirectToPage();
        }
    }
}
