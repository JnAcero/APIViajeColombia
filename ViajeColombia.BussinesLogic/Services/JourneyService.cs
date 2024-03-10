using Microsoft.EntityFrameworkCore;
using ViajeColombia.BussinesLogic.Contracts;
using ViajeColombia.DataAccess.Repositories.Contracts;
using ViajeColombia.Models;

namespace ViajeColombia.BussinesLogic.Services
{
    public class JourneyService : IJourneyService
    {
        private readonly IGenericRepository<Journey> _journeyRepository;
        public JourneyService(IGenericRepository<Journey> journeyRepo)
        {
            _journeyRepository = journeyRepo;
        }

        public async Task<bool> JourneyExistWithMaxValueInDB(string origin, string destination, int? maxValue)
        {
            var journeys = await _journeyRepository.GetAll();
            var hasPath = journeys
                .Where(j =>
                    j.Origin == origin &&
                    j.Destination == destination &&
                    j.JourneyFlights.Count <= maxValue)
                .Any();

            return hasPath;
        }

        public async Task<bool> JourneyExistInDatabase(string origin, string destination)
        {
            var query = await _journeyRepository
                .GetWhen(journey => 
                    journey.Origin == origin && 
                    journey.Destination == destination);

            return query.Any();
        }

        public async Task<List<Journey>> RetreaveJourneyFromDatabase(string origin, string destination)
        {
            var listaJourney = await _journeyRepository.GetWhen(journey => journey.Origin == origin && journey.Destination == destination);

            var response = listaJourney
                .Include(j => j.JourneyFlights)
                .ThenInclude(jf => jf.Fligth)
                .ThenInclude(f => f.Transport)
                .ToList();

            return response;
        }

        public async Task<List<Journey>> RetreaveJourneyFromDatabase(string origin, string destination, int? maxValue)
        {
            var journeys = await _journeyRepository.GetAll();
            var result = journeys
                .Where(j =>
                    j.Origin == origin &&
                    j.Destination == destination &&
                    j.JourneyFlights.Count <= maxValue)
                .Include(j => j.JourneyFlights)
                .ThenInclude(jf => jf.Fligth)
                .ThenInclude(f => f.Transport)
                .ToList();

            return result;
        }
        public async Task CreateRange(List<Journey> journeys)
        {
            await _journeyRepository.CreateRange(journeys);
        }
    }
}
