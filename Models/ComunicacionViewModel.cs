using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace ProyectoONGDBNoSQL.ViewModels
{
    public class ComunicacionViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Contenido { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public string UsuarioId { get; set; } = string.Empty;
        public List<string> UsuarioIds { get; set; } = new List<string>();
        public IEnumerable<SelectListItem> UsuariosList { get; set; } = new List<SelectListItem>();
    }
}