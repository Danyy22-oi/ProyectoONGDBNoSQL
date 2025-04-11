using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using ProyectoONGDBNoSQL.Models; 

namespace ProyectoONGDBNoSQL.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var connectionString = configuration["MongoDB:ConnectionString"];
            var databaseName = configuration["MongoDB:DatabaseName"];
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        
        public IMongoDatabase Database
        {
            get { return _database; }
        }

        
        public IMongoCollection<Usuario> Usuarios =>
            _database.GetCollection<Usuario>("usuarios");

        
    }
}

