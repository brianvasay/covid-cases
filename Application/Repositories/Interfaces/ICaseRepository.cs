using Application.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repositories.Interfaces
{
    public interface ICaseRepository
    {
        Task<IReadOnlyList<Case>> GetCasesAsync();

        Task<IReadOnlyList<Case>> GetCasesAsync(string observationDate, int maxResults);
    }
}
