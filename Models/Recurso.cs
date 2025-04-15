using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ONG.Models
{
    public class Recurso
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string IdRecurso { get; set; }

        [BsonElement("nombre_recurso")]
        public string NombreRecurso { get; set; }

        [BsonElement("tipo")]
        public string Tipo { get; set; }

        [BsonElement("unidad")]
        public string UnidadMedida { get; set; }
    }
}
