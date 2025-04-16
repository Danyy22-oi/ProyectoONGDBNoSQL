using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace ProyectoONGDBNoSQL.ViewModels
{
    public class BeneficiarioViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Contacto { get; set; } = string.Empty;
        public string Ubicacion { get; set; } = string.Empty;
        public string Necesidades { get; set; } = string.Empty;
        public string Proyectos { get; set; } = string.Empty;

        public IEnumerable<SelectListItem> BeneficiariosList { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> ProyectosList { get; set; } = new List<SelectListItem>();
    }
}
