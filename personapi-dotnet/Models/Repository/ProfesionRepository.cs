using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Models.Repository
{
    public class ProfesionRepository : IProfesionRepository
    {
        private readonly PersonaDbContext _context; // Asume que Profesion está en el mismo DbContext

        public ProfesionRepository(PersonaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Profesion>> GetAllProfesionesAsync()
        {
            return await _context.Profesions.ToListAsync();
        }

        public async Task<Profesion> GetProfesionByIdAsync(int id)
        {
            return await _context.Profesions.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddProfesionAsync(Profesion profesion)
        {
            _context.Profesions.Add(profesion);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProfesionAsync(Profesion profesion)
        {
            _context.Entry(profesion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProfesionAsync(int id)
        {
            var profesion = await _context.Profesions.FindAsync(id);
            if (profesion != null)
            {
                _context.Profesions.Remove(profesion);
                await _context.SaveChangesAsync();
            }
        }
    }
}
