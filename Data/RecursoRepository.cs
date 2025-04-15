using MongoDB.Driver;
using ONG.Models;


namespace ProyectoONGDBNoSQL.Data
{
    public class RecursoRepository
    {
        private readonly IMongoCollection<Recurso> _recursos;

        public RecursoRepository(MongoDbContext context)
        {
            _recursos = context.Database.GetCollection<Recurso>("recursos");
        }

        public async Task<List<Recurso>> GetAllAsync()
        {
            return await _recursos.Find(r => true).ToListAsync();
        }

        public async Task<Recurso> GetByIdAsync(string id)
        {
            return await _recursos.Find(r => r.IdRecurso == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Recurso recurso)
        {
            Console.WriteLine(">>> Insertando recurso: " + recurso.NombreRecurso);
            await _recursos.InsertOneAsync(recurso);
        }


        public async Task UpdateAsync(string id, Recurso recurso)
        {
            await _recursos.ReplaceOneAsync(r => r.IdRecurso == id, recurso);
        }

        public async Task DeleteAsync(string id)
        {
            await _recursos.DeleteOneAsync(r => r.IdRecurso == id);
        }
    }
}
