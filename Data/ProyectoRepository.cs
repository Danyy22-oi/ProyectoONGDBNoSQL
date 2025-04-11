using MongoDB.Driver;
using ProyectoONGDBNoSQL.Data;
using ProyectoONGDBNoSQL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProyectoONGDBNoSQL.Repositories
{
    public class ProyectoRepository
    {
        private readonly MongoDbContext _context;

        public ProyectoRepository(MongoDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Lista de proyectos.</returns>
        public async Task<List<Proyecto>> GetAllAsync()
        {
            return await _context.Database
                .GetCollection<Proyecto>("proyectos")
                .Find(p => true)
                .ToListAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id">El ID del proyecto.</param>
        /// <returns>El proyecto si se encuentra, o null.</returns>
        public async Task<Proyecto> GetByIdAsync(string id)
        {
            return await _context.Database
                .GetCollection<Proyecto>("proyectos")
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="proyecto">El proyecto a insertar.</param>
        /// <returns>Task asíncrona.</returns>
        public async Task CreateAsync(Proyecto proyecto)
        {
            await _context.Database
                .GetCollection<Proyecto>("proyectos")
                .InsertOneAsync(proyecto);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">El ID del proyecto a actualizar.</param>
        /// <param name="proyecto">El objeto proyecto con los datos actualizados.</param>
        /// <returns>Task asíncrona.</returns>
        public async Task UpdateAsync(string id, Proyecto proyecto)
        {
            await _context.Database
                .GetCollection<Proyecto>("proyectos")
                .ReplaceOneAsync(p => p.Id == id, proyecto);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">El ID del proyecto a eliminar.</param>
        /// <returns>Task asíncrona.</returns>
        public async Task DeleteAsync(string id)
        {
            await _context.Database
                .GetCollection<Proyecto>("proyectos")
                .DeleteOneAsync(p => p.Id == id);
        }

        /// <summary>
        /// </summary>
        /// <param name="voluntarioId">El ID del voluntario a eliminar de los proyectos.</param>
        /// <returns>Task asíncrona.</returns>
        public async Task RemoveVolunteerFromProjectsAsync(string voluntarioId)
        {
           
            var collection = _context.Database.GetCollection<Proyecto>("proyectos");

           
            var filter = Builders<Proyecto>.Filter.AnyEq(p => p.VoluntariosAsignados, voluntarioId);

            
            var update = Builders<Proyecto>.Update.Pull(p => p.VoluntariosAsignados, voluntarioId);

            await collection.UpdateManyAsync(filter, update);
        }
    }
}
