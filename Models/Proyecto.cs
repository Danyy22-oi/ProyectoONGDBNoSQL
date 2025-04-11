using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProyectoONGDBNoSQL.Models
{
    public class Proyecto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("nombre_proyecto")]
        public string NombreProyecto { get; set; } = string.Empty;

        [BsonElement("tipo_crisis")]
        public string TipoCrisis { get; set; } = string.Empty;

        [BsonElement("fecha_inicio")]
        public DateTime FechaInicio { get; set; }

        [BsonElement("fecha_fin")]
        [BsonIgnoreIfNull]
        public DateTime? FechaFin { get; set; }

        [BsonElement("estado")]
        public string Estado { get; set; } = string.Empty;

        [BsonElement("descripcion")]
        public string Descripcion { get; set; } = string.Empty;

        [BsonElement("recursos_asignados")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> RecursosAsignados { get; set; } = new List<string>();

        [BsonElement("voluntarios_asignados")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> VoluntariosAsignados { get; set; } = new List<string>();
    }
}
