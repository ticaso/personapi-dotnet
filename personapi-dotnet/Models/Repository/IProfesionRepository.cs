using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Models.Repository
{
    public interface IProfesionRepository
    {
        Task<Profesion> GetProfesionByIdAsync(int id);
        Task<IEnumerable<Profesion>> GetAllProfesionesAsync();
        Task AddProfesionAsync(Profesion profesion);
        Task UpdateProfesionAsync(Profesion profesion);
        Task DeleteProfesionAsync(int id);
    }
}
