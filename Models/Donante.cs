using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ProyectoONGDBNoSQL.Models
{
    
    [BsonIgnoreExtraElements]
    public class Donante
    {
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        
        [BsonElement("usuario_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UsuarioId { get; set; } = string.Empty;

        
        [BsonElement("nombre")]
        public string Nombre { get; set; } = string.Empty;

        
        [BsonElement("tipo_donante")]
        public string TipoDonante { get; set; } = string.Empty;

        
        [BsonElement("contacto")]
        public ContactoData Contacto { get; set; } = new ContactoData();

        
        [BsonElement("donaciones")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Donaciones { get; set; } = new List<string>();

        
        public class ContactoData
        {
            [BsonElement("email")]
            public string Email { get; set; } = string.Empty;

            [BsonElement("telefono")]
            public string Telefono { get; set; } = string.Empty;
        }
    }
}
