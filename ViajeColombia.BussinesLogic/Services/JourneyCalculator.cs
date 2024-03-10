using AutoMapper;
using ViajeColombia.BussinesLogic.Contracts;
using ViajeColombia.BussinesLogic.DataStructures;
using ViajeColombia.BussinesLogic.DTOs;
using ViajeColombia.BussinesLogic.Exceptions;
using ViajeColombia.Models;

namespace ViajeColombia.BussinesLogic.Services
{
    public class JourneyCalculator : IJourneyCalculator
    {
        private readonly IJourneyService _journeyService;
        private readonly IApiClientService _apiClient;
        private readonly IMapper _mapper;

        public JourneyCalculator(
            IJourneyService journeyService,
            IApiClientService apiClient,
            IMapper mapper)
        {
            _apiClient = apiClient;
            _journeyService = journeyService;
            _mapper = mapper;
        }

        public async Task<List<JourneyDTO>> CalculateJourney(string origin, string destination)
        {
            origin = origin.Trim().ToUpper();
            destination = destination.Trim().ToUpper();
            try
            {
                bool existInDB = await _journeyService.JourneyExistInDatabase(origin, destination);

                if (existInDB)
                {
                    var response = await _journeyService.RetreaveJourneyFromDatabase(origin, destination);
                    return _mapper.Map<List<JourneyDTO>>(response);
                }
                var listFlights = await _apiClient.GetJsonAsync();
                var graph = new FlightGraph(listFlights);
                HashSet<string> set = new();
                bool isPosible = graph.HasPath(origin, destination, set);

                if (isPosible)
                {
                    var trips = graph.FindPossibleTrips(origin, destination, new HashSet<string>());
                    var journeys = MapApiResponseToListJourney(trips, origin, destination);
                    await _journeyService.CreateRange(journeys);

                    return _mapper.Map<List<JourneyDTO>>(journeys);
                }
                throw new RouteDoesntExistException("La ruta que intenta calcular no es posible de realizar");
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<JourneyDTO>> CalculateJourney(string origin, string destination, int? maxFlights)
        {
            origin = origin.Trim().ToUpper();
            destination = destination.Trim().ToUpper();
            try
            {
                bool existInDB = await _journeyService.JourneyExistInDatabase(origin, destination);

                if (existInDB)
                {
                    bool isValid = await _journeyService.JourneyExistWithMaxValueInDB(origin, destination, maxFlights);
                    if (isValid)
                    {
                        var response = await _journeyService.RetreaveJourneyFromDatabase(origin, destination, maxFlights);
                        Console.WriteLine("Se obtuvo de la base de datos");
                        return _mapper.Map<List<JourneyDTO>>(response);
                    }
                    throw new RouteDoesntExistException($"La ruta que intenta calcular no es posible con {maxFlights} {(maxFlights != 1 ? "vuelos": "vuelo")}");
                }
                var listFlights = await _apiClient.GetJsonAsync();
                var graph = new FlightGraph(listFlights);
                HashSet<string> set = new();
                bool isPosible = graph.HasPath(origin, destination, set, maxFlights);

                if (isPosible)
                {
                    var trips = graph.FindPossibleTripsWithMaxValue(origin, destination, new HashSet<string>(), maxFlights);
                    var journeys = MapApiResponseToListJourney(trips, origin, destination);

                    return _mapper.Map<List<JourneyDTO>>(journeys);
                }
                throw new RouteDoesntExistException("La ruta que intenta calcular no es posible de realizar");
            }
            catch
            {
                throw;
            }
        }
        private List<Journey> MapApiResponseToListJourney(List<List<ApiResponseDTO>> trips, string origin, string destination)
        {
            List<Journey> journeys = new();

            foreach (List<ApiResponseDTO> subTrips in trips)
            {
                Journey journey = new Journey();
                List<JourneyFlight> journeyFlights = new();

                journey.Origin = origin;
                journey.Destination = destination;
                journey.Price = (decimal)subTrips.Sum(x => x.Price);
                journeyFlights = subTrips.Select(s => FromApiResponseToJourneyFlight(s)).ToList();
                journey.JourneyFlights = journeyFlights;

                journeys.Add(journey);
            }

            return journeys;
        }
        private JourneyFlight FromApiResponseToJourneyFlight(ApiResponseDTO apiResponse)
        {
            JourneyFlight journeyFlight = new()
            {
                Fligth = FromApiReponseToFlight(apiResponse)
            };
            return journeyFlight;
        }
        private Flight FromApiReponseToFlight(ApiResponseDTO apiRespone)
        {
            Flight flight = new()
            {
                Origin = apiRespone.DepartureStation,
                Destination = apiRespone.ArrivalStation,
                Price = (decimal)apiRespone.Price,
                Transport = FromApiResponseToTransport(apiRespone)
            };
            return flight;
        }
        private Transport FromApiResponseToTransport(ApiResponseDTO apiRespone)
        {
            Transport transport = new()
            {
                FlightCarrier = apiRespone.FlightCarrier,
                FlightNumber = apiRespone.FlightNumber,
            };
            return transport;
        }
    }
}
