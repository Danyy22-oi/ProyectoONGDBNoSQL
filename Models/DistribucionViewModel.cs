using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace ProyectoONGDBNoSQL.ViewModels
{
    public class DistribucionViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string ProyectoId { get; set; } = string.Empty;
        public DateTime FechaEnvio { get; set; } = DateTime.UtcNow;
        public string Destino { get; set; } = string.Empty;
        public string RecursoId { get; set; } = string.Empty;
        public IEnumerable<SelectListItem> RecursoIdList { get; set; } = new List<SelectListItem>();
        public string ResponsableId { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public IEnumerable<SelectListItem> ProyectosList { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> ResponsablesList { get; set; } = new List<SelectListItem>();
    }
}