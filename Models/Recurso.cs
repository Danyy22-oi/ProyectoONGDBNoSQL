using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ONG.Models
{
    public class Recurso
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string IdRecurso { get; set; } = string.Empty;

        [BsonElement("nombre_recurso")]
        public string NombreRecurso { get; set; } = string.Empty;

        [BsonElement("tipo")]
        public string Tipo { get; set; } = string.Empty;

        [BsonElement("unidad")]
        public string UnidadMedida { get; set; } = string.Empty;
    }
}
