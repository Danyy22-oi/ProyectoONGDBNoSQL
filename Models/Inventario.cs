using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ONG.Models
{
    public class Inventario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string IdInventario { get; set; }

        [BsonElement("recurso_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string IdRecurso { get; set; }

        [BsonElement("cantidad_disponible")]
        public int CantidadDisponible { get; set; }

        [BsonElement("ubicacion")]
        public string Ubicacion { get; set; }
    }
}
