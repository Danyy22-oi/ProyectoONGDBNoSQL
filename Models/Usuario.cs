using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProyectoONGDBNoSQL.Models
{
    public class Usuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [BsonElement("apellido")]
        public string Apellido { get; set; } = string.Empty;

        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("contraseña")]
        public string Contraseña { get; set; } = string.Empty;

        [BsonElement("rol")]
        public string Rol { get; set; } = string.Empty;

        [BsonElement("estado")]
        public string Estado { get; set; } = string.Empty;
    }

}
