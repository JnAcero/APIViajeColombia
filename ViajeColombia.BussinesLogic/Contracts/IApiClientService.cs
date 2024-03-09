using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViajeColombia.BussinesLogic.DTOs;
using ViajeColombia.Models;

namespace ViajeColombia.BussinesLogic.Contracts
{
    public interface IApiClientService
    {
        Task<List<ApiResponseDTO>> GetJsonAsync();

        Task<Object> CalculateJourney(string origin, string destination);
    }
}
