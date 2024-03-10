using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViajeColombia.Models;

namespace ViajeColombia.BussinesLogic.Contracts
{
    public interface IJourneyService
    {
        Task<bool> JourneyExistInDatabase(string origin, string destination);
        Task<List<Journey>> RetreaveJourneyFromDatabase(string origin, string destination);
        Task<bool> JourneyExistWithMaxValueInDB(string origin, string destination, int? maxValue);
        Task<List<Journey>> RetreaveJourneyFromDatabase(string origin, string destination, int? maxValue);
        Task CreateRange(List<Journey> journeys);
    }
}
