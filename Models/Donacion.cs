using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ProyectoONGDBNoSQL.Models
{
    public class Donacion
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        
        [BsonElement("donante_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string DonanteId { get; set; } = string.Empty;

        
        [BsonElement("fecha_donacion")]
        public DateTime FechaDonacion { get; set; } = DateTime.UtcNow;

       
        [BsonElement("tipo")]
        public string Tipo { get; set; } = string.Empty;

        
        [BsonElement("detalle")]
        public string Detalle { get; set; } = string.Empty;

        
        [BsonElement("estado")]
        public string Estado { get; set; } = string.Empty;
    }
}
