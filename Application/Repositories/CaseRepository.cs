using Application.Data;
using Application.Entities;
using Application.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public class CaseRepository : ICaseRepository
    {
        private readonly ApplicationDbContext _context;
        public CaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Case>> GetCasesAsync()
        {
            return await _context.Cases
                .GroupBy(c => new { c.CountryRegion, c.ObservationDate })
                    .Select(x => new Case
                    {
                        ObservationDate = x.Key.ObservationDate,
                        CountryRegion = x.Key.CountryRegion,
                        Confirmed = x.Sum(y => y.Confirmed),
                        Deaths = x.Sum(y => y.Deaths),
                        Recovered = x.Sum(y => y.Recovered)
                    })
                    .OrderByDescending(c => c.ObservationDate)
                        .ThenBy(c => c.CountryRegion)
                            .ToListAsync();
        }

        public async Task<IReadOnlyList<Case>> GetCasesAsync(string observationDate, int maxResults)
        {
            return await _context.Cases
                .GroupBy(c => new { c.CountryRegion, c.ObservationDate })
                    .Select(x => new Case
                    {
                        ObservationDate = x.Key.ObservationDate,
                        CountryRegion = x.Key.CountryRegion,
                        Confirmed = x.Sum(y => y.Confirmed),
                        Deaths = x.Sum(y => y.Deaths),
                        Recovered = x.Sum(y => y.Recovered)
                    })
                    .Where(c => c.ObservationDate == Convert.ToDateTime(observationDate))
                        .OrderByDescending(c => c.ObservationDate)
                            .ThenBy(c => c.CountryRegion)
                                .Take(maxResults)
                                    .ToListAsync();
        }
    }
}
