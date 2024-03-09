using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViajeColombia.BussinesLogic.Contracts;
using ViajeColombia.BussinesLogic.DTOs;
using ViajeColombia.BussinesLogic.Exceptions;

namespace ViajeColombia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JourneyController : ControllerBase
    {
        private readonly IApiClientService _apiClientService;

        public JourneyController(IApiClientService apiClientService)
        {
            _apiClientService = apiClientService;
        }

        [HttpPost]
        [Route("Calculate")]
        public async Task<ActionResult> Calculate([FromBody]DetailsJourneyDTO details)
        {
            try
            {
                var response = await _apiClientService.CalculateJourney(details.Origin,details.Destination);

                if (response is null)
                {
                    return NotFound();
                }

                return Ok(new
                {
                    Journeys = response
                });
            }
            catch (RouteDoesntExistException rex)
            {
                return BadRequest(new
                {
                    message = rex.Message
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }
        [HttpGet]
        [Route("List")]
        public async Task<ActionResult> List()
        {
            try
            {
                var response = await _apiClientService.GetJsonAsync();

                if (response is null)
                {
                    return NotFound();
                }

                return Ok(response);
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }

        }
    }
}
