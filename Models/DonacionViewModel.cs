using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoONGDBNoSQL.ViewModels
{
    public class DonacionViewModel
    {
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar un donante")]
        public string DonanteId { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de donación es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaDonacion { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "El tipo de donación es obligatorio")]
        public string Tipo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El detalle es obligatorio")]
        
        [RegularExpression(@".*[\$₡€].*",
             ErrorMessage = "El detalle debe incluir uno de estos símbolos: $, ₡ o €")]
        public string Detalle { get; set; } = string.Empty;

        [Required(ErrorMessage = "El estado es obligatorio")]
        public string Estado { get; set; } = string.Empty;

        public IEnumerable<SelectListItem> DonantesList { get; set; } = new List<SelectListItem>();

       
        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [RegularExpression(@"^[+\-0-9]{7,}$",
            ErrorMessage = "El teléfono debe tener al menos 7 caracteres y sólo puede contener dígitos, + o -")]
        public string ContactoTelefono { get; set; } = string.Empty;
    }
}
