using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ProyectoONGDBNoSQL.Models
{
    public class RecursoEnviado
{
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("recurso_id")]  // Asegúrate de que este nombre coincida con el campo de MongoDB
    public required string RecursoId { get; set; }
    
    [BsonElement("cantidad")]  // Asegúrate de que este nombre coincida con el campo de MongoDB
    public int Cantidad { get; set; }
}

    public class Distribucion
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        
        [BsonElement("proyecto_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProyectoId { get; set; } = string.Empty;

        
        [BsonElement("fecha_envio")]
        public DateTime FechaEnvio { get; set; } = DateTime.UtcNow;


       
        [BsonElement("destino")]
        public string Destino { get; set; } = string.Empty;


        [BsonElement("recursos_enviados")]
        public List<RecursoEnviado> RecursoEnviados { get; set; } = new List<RecursoEnviado>();

        [BsonElement("responsable")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ResponsableId { get; set; } = string.Empty;

        
        [BsonElement("estado")]
        public string Estado { get; set; } = string.Empty;
    }
}
