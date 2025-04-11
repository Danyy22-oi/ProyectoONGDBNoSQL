using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace ProyectoONGDBNoSQL.ViewModels
{
    public class VoluntarioViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string InfoUsuarioId { get; set; } = string.Empty;
        public string HabilidadesText { get; set; } = string.Empty;
        public string Disponibilidad { get; set; } = string.Empty;
        public List<string> ProyectosSeleccionados { get; set; } = new List<string>();
        public IEnumerable<SelectListItem> UsuariosList { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> ProyectosList { get; set; } = new List<SelectListItem>();
    }
}
