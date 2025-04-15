using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace ONG.Models
{
    public class Inventario
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        [BindNever] 
        public string? IdInventario { get; set; }

        [BsonElement("recurso_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        [Required]
        public string IdRecurso { get; set; }

        [BsonElement("cantidad_disponible")]
        [Required]
        public int CantidadDisponible { get; set; }

        [BsonElement("ubicacion")]
        [Required]
        public string Ubicacion { get; set; }
    }
}
