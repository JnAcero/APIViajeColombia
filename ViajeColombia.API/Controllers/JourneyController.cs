using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViajeColombia.API.Utility;
using ViajeColombia.BussinesLogic.Contracts;
using ViajeColombia.BussinesLogic.DTOs;
using ViajeColombia.BussinesLogic.Exceptions;

namespace ViajeColombia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JourneyController : ControllerBase
    {
        private readonly IJourneyCalculator _journeyCalculator;

        public JourneyController(IJourneyCalculator journeyCalculator)
        {
            _journeyCalculator = journeyCalculator;
        }

        [HttpPost]
        [Route("Calculate")]
        public async Task<ActionResult> Calculate([FromBody]DetailsJourneyDTO details)
        {
            ApiResponse<object> response = new();
            try
            {
                if (details.NumberOfFligths is null)
                {
                    response.Response = await _journeyCalculator.CalculateJourney(details.Origin, details.Destination);
                    response.IsSuccess = true;
                }
                else
                {
                    response.Response = await _journeyCalculator.CalculateJourney(details.Origin, details.Destination, details.NumberOfFligths);
                    response.IsSuccess = true;
                }

                return Ok(response);
            }
            catch (InvalidDestinationException ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = false;
                return BadRequest(response);
            }
            catch (RouteDoesntExistException ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = false;
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }
    }
}
