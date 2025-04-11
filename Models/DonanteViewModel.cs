using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ProyectoONGDBNoSQL.ViewModels
{
    public class DonanteViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string TipoDonante { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;

        public IEnumerable<SelectListItem> TipoDonanteList { get; set; } = new List<SelectListItem>();
    }
}
