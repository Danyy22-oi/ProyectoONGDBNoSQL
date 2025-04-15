using MongoDB.Driver;
using ONG.Models;

namespace ProyectoONGDBNoSQL.Data
{
    public class InventarioRepository
    {
        private readonly IMongoCollection<Inventario> _inventarios;

        public InventarioRepository(MongoDbContext context)
        {
            _inventarios = context.Database.GetCollection<Inventario>("inventario");
        }

        public async Task<List<Inventario>> GetAllAsync()
        {
            return await _inventarios.Find(_ => true).ToListAsync();
        }

        public async Task<Inventario> GetByIdAsync(string id)
        {
            return await _inventarios.Find(i => i.IdInventario == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Inventario inventario)
        {
            await _inventarios.InsertOneAsync(inventario);
        }

        public async Task UpdateAsync(string id, Inventario inventario)
        {
            await _inventarios.ReplaceOneAsync(i => i.IdInventario == id, inventario);
        }

        public async Task DeleteAsync(string id)
        {
            await _inventarios.DeleteOneAsync(i => i.IdInventario == id);
        }
    }
}
