using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ProyectoONGDBNoSQL.Models
{
    public class Comunicacion
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("tipo")]
        public string Tipo { get; set; } = string.Empty;
        

        [BsonElement("contenido")]
        public string Contenido { get; set; } = string.Empty;
        
        [BsonElement("fecha_creacion")]
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        [BsonElement("usuarios_destinatarios")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> UsuarioId { get; set; } = new List<string>();
    }
}
