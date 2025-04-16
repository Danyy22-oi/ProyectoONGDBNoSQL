using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ProyectoONGDBNoSQL.Models
{
    public class Incidencia
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        
        [BsonElement("incidencia_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string IncidenciaId { get; set; } = string.Empty;

        [BsonElement("tipo_incidencia")]
        public string TipoIncidencia { get; set; } = string.Empty;
        
        [BsonElement("descripcion")]
        public string Descripcion { get; set; } = string.Empty;
        
        [BsonElement("fecha_reporte")]
        public DateTime FechaReporte { get; set; } = DateTime.UtcNow;

        [BsonElement("proyecto_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProyectoId { get; set; } = string.Empty;

        [BsonElement("distribucion_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string DistrbucionId { get; set; } = string.Empty;

        [BsonElement("estado")]
        public string Estado { get; set; } = string.Empty;
        [BsonElement("responsable")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ResponsableId { get; set; } = string.Empty;
    }
}
