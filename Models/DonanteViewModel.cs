using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoONGDBNoSQL.ViewModels
{
    public class DonanteViewModel
    {
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar un tipo de donante")]
        public string TipoDonante { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "El email no tiene un formato válido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [RegularExpression(@"^[+\-0-9]{7,}$",
            ErrorMessage = "El teléfono debe tener al menos 7 caracteres y solo puede contener dígitos, + o -")]
        public string Telefono { get; set; } = string.Empty;

        public IEnumerable<SelectListItem> TipoDonanteList { get; set; } = new List<SelectListItem>();
    }
}
