using ViajeColombia.BussinesLogic.DTOs;
using ViajeColombia.BussinesLogic.Exceptions;

namespace ViajeColombia.BussinesLogic.DataStructures
{
    public class FlightGraph
    {
        private Dictionary<string, List<ApiResponseDTO>> adjacencyList;
        public FlightGraph(List<ApiResponseDTO> flights)
        {
            this.adjacencyList = new();

            foreach (var flight in flights)
            {
                if (!adjacencyList.ContainsKey(flight.DepartureStation))
                {
                    adjacencyList[flight.DepartureStation] = new();
                }
                if (!adjacencyList.ContainsKey(flight.ArrivalStation))
                {
                    adjacencyList[flight.ArrivalStation] = new();
                }
                adjacencyList[flight.DepartureStation].Add(flight);
            }
        }
        public bool HasPath(string origin, string destination, HashSet<string> visited)
        {
            try
            {
                var existCurrentKey = adjacencyList.TryGetValue(origin, out var currentKey);

                if (!existCurrentKey) throw new InvalidDestinationException("Origen o destino inexistente");
                if (origin == destination) return true;
                if (visited.Contains(origin)) return false;

                visited.Add(origin);

                foreach (ApiResponseDTO neighbor in currentKey)
                {
                    if (HasPath(neighbor.ArrivalStation, destination, visited) == true)
                        return true;
                }

                return false;
            }
            catch(Exception ex)
            {
                throw;
            }

        }

        public bool HasPath(string origin, string destination, HashSet<string> visited, int? maxFlights)
        {
            try
            {
                var existCurrentKey = adjacencyList.TryGetValue(origin, out var currentKey);

                if (!existCurrentKey) throw new InvalidDestinationException("Origen o destino inexistente");
                if (origin == destination) return true;
                if (visited.Contains(origin) || maxFlights <= 0) return false;

                visited.Add(origin);
                int count = 0;

                foreach (ApiResponseDTO neighbor in currentKey)
                {
                    if (HasPath(neighbor.ArrivalStation, destination, visited, maxFlights - 1) == true)
                    {
                        count++;
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public int CountPossibleTrips(string origin, string destination, HashSet<string> visited)
        {
            if (origin == destination) return 1;
            if (visited.Contains(origin)) return 0;

            visited.Add(origin);

            int count = 0;

            foreach (ApiResponseDTO neighbor in adjacencyList[origin])
            {
                count += CountPossibleTrips(neighbor.ArrivalStation, destination, visited);
            }

            visited.Remove(origin); // Backtrack

            return count;
        }
        public List<List<ApiResponseDTO>> FindPossibleTrips(string origin, string destination, HashSet<string> visited)
        {
            if (origin == destination)
            {
                // Si el origen y el destino son iguales, se ha encontrado un posible camino
                return new List<List<ApiResponseDTO>> { new List<ApiResponseDTO>() };
            }
            if (visited.Contains(origin))
            {
                // Evitar ciclos
                return new List<List<ApiResponseDTO>>();
            }

            visited.Add(origin);

            List<List<ApiResponseDTO>> possibleTrips = new List<List<ApiResponseDTO>>();

            foreach (ApiResponseDTO neighbor in adjacencyList[origin])
            {
                List<List<ApiResponseDTO>> subPaths = FindPossibleTrips(neighbor.ArrivalStation, destination, visited);

                foreach (List<ApiResponseDTO> subPath in subPaths)
                {
                    // Agregar el vuelo actual al inicio de cada subruta encontrada
                    subPath.Insert(0, neighbor);

                    possibleTrips.Add(subPath);
                }
            }

            visited.Remove(origin); // Backtrack

            return possibleTrips;
        }
        public List<List<ApiResponseDTO>> FindPossibleTripsWithMaxValue(string origin, string destination, HashSet<string> visited, int? maxFlights)
        {
            if (origin == destination)
            {
                return new List<List<ApiResponseDTO>>() { new List<ApiResponseDTO>() }; // Devolver una lista con un viaje vacío
            }

            if (visited.Contains(origin) || maxFlights <= 0)
            {
                return null; // No se puede continuar desde este nodo
            }

            visited.Add(origin);

            var existCurrentKey = adjacencyList.TryGetValue(origin, out var currentKey);
            if (!existCurrentKey) return null;

            List<List<ApiResponseDTO>> trips = new List<List<ApiResponseDTO>>();

            foreach (ApiResponseDTO neighbor in currentKey)
            {
                var nextTrips = FindPossibleTripsWithMaxValue(neighbor.ArrivalStation, destination, visited, maxFlights - 1);
                if (nextTrips != null)
                {
                    foreach (var trip in nextTrips)
                    {
                        trip.Insert(0, neighbor); // Insertar el vuelo actual al inicio del viaje
                        trips.Add(trip);
                    }
                }
            }
            visited.Remove(origin); // Backtrack

            return trips;
        }
    }
}
