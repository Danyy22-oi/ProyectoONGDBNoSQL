using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ProyectoONGDBNoSQL.Models
{
    public class Beneficiario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        
        [BsonElement("nombre")]
        public string Nombre { get; set; } = string.Empty;

       
        [BsonElement("contacto")]
        public string Contacto { get; set; } = string.Empty;

        
        [BsonElement("ubicacion")]
        public string Ubicacion { get; set; } = string.Empty;

        
        [BsonElement("necesidades")]
        public List<string> Necesidades { get; set; } = new List<string>();

        [BsonElement("proyectos_asociados")]
        public List<ObjectId> Proyectos { get; set; } = new List<ObjectId>();

    }
}
