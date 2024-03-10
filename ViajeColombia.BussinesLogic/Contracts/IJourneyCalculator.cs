using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViajeColombia.BussinesLogic.DTOs;

namespace ViajeColombia.BussinesLogic.Contracts
{
    public interface IJourneyCalculator
    {
        Task<List<JourneyDTO>> CalculateJourney(string origin, string destination);
        Task<List<JourneyDTO>> CalculateJourney(string origin, string destination, int? maxFlights);

    }
}
