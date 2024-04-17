﻿using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Models.Repository
{
    public interface IPersonaRepository
    {
        Task<Persona> GetPersonaByIdAsync(int cc);
        Task<IEnumerable<Persona>> GetAllPersonasAsync();
        Task AddPersonaAsync(Persona persona);
        Task UpdatePersonaAsync(Persona persona);
        Task DeletePersonaAsync(int cc);
    }
}


