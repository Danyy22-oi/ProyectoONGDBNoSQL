using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace ProyectoONGDBNoSQL.ViewModels
{
    public class IncidenciaViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string IncidenciaId { get; set; } = string.Empty;
        public string TipoIncidencia { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaReporte { get; set; } = DateTime.UtcNow;
        public string ProyectoId { get; set; } = string.Empty;
        public string DistrbucionId { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string ResponsableId { get; set; } = string.Empty;
        public IEnumerable<SelectListItem> ProyectosList { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> DistribucionesList { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> ResponsablesList { get; set; } = new List<SelectListItem>();
    }
}