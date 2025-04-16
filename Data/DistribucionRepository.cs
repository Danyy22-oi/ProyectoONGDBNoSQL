using MongoDB.Driver;
using ProyectoONGDBNoSQL.Data;
using ProyectoONGDBNoSQL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProyectoONGDBNoSQL.Repositories
{
    public class DistribucionRepository
    {
        private readonly MongoDbContext _context;

        public DistribucionRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<Distribucion>> GetAllAsync()
        {
            return await _context.Database
                .GetCollection<Distribucion>("distribuciones")
                .Find(d => true)
                .ToListAsync();
        }

        public async Task<Distribucion> GetByIdAsync(string id)
        {
            return await _context.Database
                .GetCollection<Distribucion>("distribuciones")
                .Find(d => d.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Distribucion distribucion)
        {
            await _context.Database
                .GetCollection<Distribucion>("distribuciones")
                .InsertOneAsync(distribucion);
        }

        public async Task UpdateAsync(string id, Distribucion distribucion)
        {
            await _context.Database
                .GetCollection<Distribucion>("distribuciones")
                .ReplaceOneAsync(d => d.Id == id, distribucion);
        }

        public async Task DeleteAsync(string id)
        {
            await _context.Database
                .GetCollection<Distribucion>("distribuciones")
                .DeleteOneAsync(d => d.Id == id);
        }
    }
}