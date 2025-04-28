using InversionesWebUI.Model;
using InversionesWebUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InversionesWebUI.Pages
{
    public class CreateModel(IProductosService productosService, IInversionesService inversionesService) : PageModel
    {
        private readonly IProductosService _productosService = productosService;
        private readonly IInversionesService _inversionesService = inversionesService;

        public List<Producto> Productos { get; set; } = new();

        [BindProperty]
        public int SelectedProductId { get; set; }

        public List<SelectListItem> OpcionProductos { get; set; } = new();

        public async Task OnGetAsync()
        {
            Productos = await _productosService.GetProductos();

            // Llenar el dropdown 
            OpcionProductos = Productos.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Nombre
            }).ToList();
            OpcionProductos.Insert(0, new SelectListItem
            {
                Value = "",
                Text = "-- Seleccione un Producto --",
                Selected = true
            });
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (SelectedProductId == 0)
            {
                return Page();
            }

            await _inversionesService.Create(new 
                Inscripcion{IdCliente = 1, IdProducto = SelectedProductId,Id=""});

            return RedirectToPage("Inscripciones");
        }
    }
}
