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
        [EmailAddress(ErrorMessage = "El email no tiene un formato v�lido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tel�fono es obligatorio")]
        [RegularExpression(@"^[+\-0-9]{7,}$",
            ErrorMessage = "El tel�fono debe tener al menos 7 caracteres y solo puede contener d�gitos, + o -")]
        public string Telefono { get; set; } = string.Empty;

        public IEnumerable<SelectListItem> TipoDonanteList { get; set; } = new List<SelectListItem>();
    }
}
