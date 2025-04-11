using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ProyectoONGDBNoSQL.Models
{
    public class Voluntario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("info_usuario")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string InfoUsuarioId { get; set; } = string.Empty;

        [BsonElement("habilidades")]
        public List<string> Habilidades { get; set; } = new List<string>();

        [BsonElement("disponibilidad")]
        public string Disponibilidad { get; set; } = string.Empty;

        [BsonElement("historial_proyectos")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> HistorialProyectos { get; set; } = new List<string>();
    }

}
