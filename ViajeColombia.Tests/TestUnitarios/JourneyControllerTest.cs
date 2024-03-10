using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViajeColombia.API.Controllers;
using ViajeColombia.BussinesLogic.Contracts;
using ViajeColombia.BussinesLogic.DTOs;
using ViajeColombia.BussinesLogic.Services;
using ViajeColombia.DataAccess.Repositories.Contracts;
using ViajeColombia.Models;

namespace ViajeColombia.Tests.TestUnitarios
{
    [TestClass]
    public class JourneyControllerTest
    {
        private readonly JourneyService _js;
        private readonly Mock<IGenericRepository<Journey>> _journeyRepoMock = new();
        private readonly Mock<IJourneyCalculator> _journeyCalculatorMock = new();
        private readonly JourneyController _journeyController;
        private Fixture _fixture;

        public JourneyControllerTest(JourneyService journeyService)
        {
            _fixture = new Fixture();
            _js = new JourneyService(_journeyRepoMock.Object);
            _journeyController = new JourneyController(_journeyCalculatorMock.Object);
        }

        [TestMethod]
        public async Task Calculate_WhenExistJourney_ReturnOK()
        {
            var origin = "STA";
            var destination = "CUC";
            var jouneyList = new List<JourneyDTO>
            {
                new JourneyDTO
                {
                    Origin = origin,
                    Destination = destination,
                    Price = 1200,
                    Fligths = new List<FlightDTO>
                    {
                       new FlightDTO
                       {
                               Origin = origin,
                               Destination = destination,
                               Price = 1200,
                               Transport = new TransportDTO
                               {
                                   FlightCarrier = "AV",
                                   FlightNumber = "18925"
                               }
                       }  
                    }
                }
            };
            _journeyCalculatorMock.Setup(calc => calc.CalculateJourney(origin, destination)).ReturnsAsync(jouneyList);

            var resut = await _journeyController.Calculate(
                new DetailsJourneyDTO
                {
                    Origin = origin,
                    Destination = destination
                });
            var obj = resut as ObjectResult;

            Assert.AreEqual(200, obj.StatusCode);
        }
    }
}
