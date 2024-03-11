using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text.Json;
using ViajeColombia.BussinesLogic.Contracts;
using ViajeColombia.BussinesLogic.DTOs;


namespace ViajeColombia.BussinesLogic.Clients
{
    public class ApiClient: IApiClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ApiClient(IHttpClientFactory httpClient)
        {
            _httpClientFactory = httpClient;
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
    }
}
