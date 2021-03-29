using Application.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Application.Data
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                // This will seed the covid_observations table.
                if (!context.Cases.Any())
                {
                    // This will read the covid_19_seed_data.json file.
                    var caseData =
                        File.ReadAllText($"{path}/Data/Seed/covid_19_seed_data.json");
                    var casesToAdd = new List<Case>();
                    // This will deserialize the json data and add the contents into an object.
                    dynamic cases = JsonConvert.DeserializeObject(caseData);
                    foreach (var item in cases)
                    {
                        casesToAdd.Add(new Case
                        {
                            Id = Convert.ToInt32(item["id"]),
                            ObservationDate = Convert.ToDateTime(item["observation_date"]),
                            ProvinceState = item["province_state"].ToString(),
                            CountryRegion = item["country_region"].ToString(),
                            Confirmed = Convert.ToDouble(item["confirmed"]),
                            Deaths = Convert.ToDouble(item["deaths"]),
                            Recovered = Convert.ToDouble(item["recovered"])
                        });
                    }
                    // The following will add the data into the database.
                    await context.Cases.AddRangeAsync(casesToAdd);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // This will create a new logger instance for the store context seed class.
                var logger = loggerFactory.CreateLogger<ApplicationDbContext>();
                // This will log the details of the exception thrown during the database seeding.
                logger.LogError(ex, "An error occurred during database seeding.");
            }
        }
    }
}
