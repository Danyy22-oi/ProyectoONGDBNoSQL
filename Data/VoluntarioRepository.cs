using MongoDB.Driver;
using ProyectoONGDBNoSQL.Data;
using ProyectoONGDBNoSQL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProyectoONGDBNoSQL.Repositories
{
    public class VoluntarioRepository
    {
        private readonly MongoDbContext _context;

        public VoluntarioRepository(MongoDbContext context)
        {
            _context = context;
        }


        public async Task<List<Voluntario>> GetAllAsync()
        {
            return await _context.Database
                .GetCollection<Voluntario>("voluntarios")
                .Find(v => true)
                .ToListAsync();
        }

        public async Task<Voluntario> GetByIdAsync(string id)
        {
            return await _context.Database
                .GetCollection<Voluntario>("voluntarios")
                .Find(v => v.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Voluntario voluntario)
        {
            await _context.Database
                .GetCollection<Voluntario>("voluntarios")
                .InsertOneAsync(voluntario);
        }

        public async Task UpdateAsync(string id, Voluntario voluntario)
        {
            await _context.Database
                .GetCollection<Voluntario>("voluntarios")
                .ReplaceOneAsync(v => v.Id == id, voluntario);
        }

        public async Task DeleteAsync(string id)
        {
            await _context.Database
                .GetCollection<Voluntario>("voluntarios")
                .DeleteOneAsync(v => v.Id == id);
        }

        public async Task<Voluntario> GetByUserIdAsync(string userId)
        {
            return await _context.Database
                .GetCollection<Voluntario>("voluntarios")
                .Find(v => v.InfoUsuarioId == userId)
                .FirstOrDefaultAsync();
        }

      
        public async Task RemoveProjectFromVolunteersAsync(string projectId)
        {
            var collection = _context.Database.GetCollection<Voluntario>("voluntarios");

            
            var filter = Builders<Voluntario>.Filter.AnyEq(v => v.HistorialProyectos, projectId);

           
            var update = Builders<Voluntario>.Update.Pull(v => v.HistorialProyectos, projectId);

            await collection.UpdateManyAsync(filter, update);
        }
    }
}
