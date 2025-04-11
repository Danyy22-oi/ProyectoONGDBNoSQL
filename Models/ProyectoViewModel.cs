using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace ProyectoONGDBNoSQL.ViewModels
{
    public class ProyectoViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string NombreProyecto { get; set; } = string.Empty;
        public string TipoCrisis { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        public List<string> VoluntariosSeleccionados { get; set; } = new List<string>();

       
        public IEnumerable<SelectListItem> UsuariosList { get; set; } = new List<SelectListItem>();
    }
}
