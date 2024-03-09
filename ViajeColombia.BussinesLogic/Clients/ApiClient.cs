using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ViajeColombia.BussinesLogic.Contracts;
using ViajeColombia.BussinesLogic.DataStructures;
using ViajeColombia.BussinesLogic.DTOs;
using ViajeColombia.BussinesLogic.Exceptions;
using ViajeColombia.DataAccess.Repositories.Contracts;
using ViajeColombia.Models;

namespace ViajeColombia.BussinesLogic.Clients
{
    public class ApiClient: IApiClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IGenericRepository<Journey> _journeyRepository;
        private readonly IMapper _mapper;

        public ApiClient(
            IHttpClientFactory httpClient,
            IGenericRepository<Journey> journeyRepo,
            IMapper mapper)
        {
            _httpClientFactory = httpClient;
            _journeyRepository = journeyRepo;
            _mapper = mapper;
        }
        private async Task<bool> JourneyExistInDatabase(string origin, string destination)
        {
            var query = await _journeyRepository.GetWhen(journey => journey.Origin == origin && journey.Destination == destination);
            return query.Any();
        }

        private async Task<List<Journey>> RetreaveJourneyFromDatabase(string origin, string destination)
        {
            var listaJourney = await _journeyRepository.GetWhen(journey => journey.Origin == origin && journey.Destination == destination);

            var response = listaJourney
                .Include(j => j.JourneyFlights)
                .ThenInclude(jf => jf.Fligth)
                .ToList();

            return response;
        }
        public async Task<List<ApiResponseDTO>> GetJsonAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("vuelos");
                var response = await client.GetAsync(""); // Realizar la solicitud HTTP
                response.EnsureSuccessStatusCode(); // Lanzar una excepción si la solicitud no tiene éxito

                var json = await response.Content.ReadAsStringAsync(); // Leer la respuesta JSON como una cadena

                // Deserializar manualmente la cadena JSON en una lista de ApiResponseDTO
                var result = JsonSerializer.Deserialize<List<ApiResponseDTO>>(json, new JsonSerializerOptions
                {
                    AllowTrailingCommas = true // Permitir comas finales en el JSON
                });

                return result;
            }
            catch
            {
                throw;
            }
        }
        public async Task<object> CalculateJourney(string origin, string destination)
        {
            try
            {
                bool existInDB = await JourneyExistInDatabase(origin, destination);

                if (existInDB)
                {
                    var response = await RetreaveJourneyFromDatabase(origin, destination);
                    return _mapper.Map<List<JourneyDTO>>(response);
                }
                var listFlights = await GetJsonAsync();
                var graph = new FlightGraph(listFlights);
                HashSet<string> set = new();
                bool isPosible = graph.HasPath(origin, destination, set);

                if (isPosible)
                {
                    var trips = graph.FindPossibleTrips(origin, destination, new HashSet<string>());

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

                    await _journeyRepository.CreateRange(journeys);

                    return _mapper.Map<List<JourneyDTO>>(journeys);
                }

                throw new RouteDoesntExistException("La ruta que intenta calcular no es posible de realizar");

            }
            catch
            {
                throw;
            }

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
