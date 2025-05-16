using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using CarInventory.Dtos;
using modelDB;
using Microsoft.AspNetCore.Identity;
using CarInventory.Models;
using Make = modelDB.Make;
using CarModel = modelDB.CarModel;
using Microsoft.AspNetCore.Authorization;

namespace CarInventory.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController(
    SourceContext context, 
    IHostEnvironment environment, 
    UserManager<SourceContextUser> userManager) : ControllerBase
    {
        private readonly SourceContext _context = context;
        private readonly string _seedFilePath = Path.Combine(environment.ContentRootPath, "Data", "CarData.csv");

        [HttpPost("Users")]
        public async Task ImportUsersAsync()
        {
            SourceContextUser user = new()
            {
                UserName = "user",
                Email = "user@gmail.com",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            IdentityResult x = await userManager.CreateAsync(user, "Passw0rd!");

            int y = await context.SaveChangesAsync();
        }


       [HttpPost("MakesFromCsv")]
        public async Task<ActionResult> ImportMakesAsync()
        {
            Dictionary<string, Make> makesByName = _context.Makes
                .AsNoTracking()
                .ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null,
                MissingFieldFound = null,
                TrimOptions = TrimOptions.Trim
            };


            using StreamReader reader = new(_seedFilePath);
            using CsvReader csv = new(reader, config);
            csv.Read();
                csv.ReadHeader();
                Console.WriteLine("Csv headers:");
                foreach (var header in csv.HeaderRecord)
                {
                    Console.WriteLine($"'{header}'");
                }

            List<CarDataDTO> records = csv.GetRecords<CarDataDTO>().ToList();
            foreach (CarDataDTO record in records)
                {

                if (makesByName.ContainsKey(record.MakeName))
                    continue;

                Make make = new()
                {
                    Name = record.MakeName,
                    Country = record.Country,
                    FoundedYear = record.FoundedYear
                };

                await _context.Makes.AddAsync(make);
                makesByName.Add(record.MakeName, make);
                }

            Console.WriteLine(_context.Database.GetDbConnection().ConnectionString);
            await _context.SaveChangesAsync();  
            return new JsonResult(makesByName.Count);
        }







        [HttpPost("ModelsFromCsv")]
        public async Task<ActionResult> ImportModelsAsync()
        {
            Dictionary<string, Make> makes = await _context.Makes
                .ToDictionaryAsync(m => m.Name);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };

            int modelCount = 0;

            using StreamReader reader = new(_seedFilePath);
            using CsvReader csv = new(reader, config);

            IEnumerable<CarDataDTO> records = csv.GetRecords<CarDataDTO>();
            foreach (CarDataDTO record in records)
            {
                if (!makes.TryGetValue(record.MakeName, out Make? make))
                {
                    Console.WriteLine($"Make not found for model {record.CarName}");
                    return NotFound(record);
                }

                CarModel model = new()
                {
                    Name = record.CarName,
                    Year = record.Year,
                    Engine = record.Engine,
                    Transmission = record.Transmission,
                    BodyStyle = record.BodyStyle,
                    Price = record.Price,
                    MakeId = make.Id
                };

                _context.CarModels.Add(model);
                modelCount++;
            }

            await _context.SaveChangesAsync();
            return new JsonResult(modelCount);
        }


    }
}
