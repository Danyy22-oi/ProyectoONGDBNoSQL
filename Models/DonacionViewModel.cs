using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace ProyectoONGDBNoSQL.ViewModels
{
    public class DonacionViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string DonanteId { get; set; } = string.Empty;
        public DateTime FechaDonacion { get; set; } = DateTime.UtcNow;
        public string Tipo { get; set; } = string.Empty;
        public string Detalle { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;

        public IEnumerable<SelectListItem> DonantesList { get; set; } = new List<SelectListItem>();

       
        public string ContactoTelefono { get; set; } = string.Empty;
    }
}
