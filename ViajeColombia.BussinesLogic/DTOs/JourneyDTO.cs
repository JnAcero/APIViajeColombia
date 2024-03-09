using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViajeColombia.Models;

namespace ViajeColombia.BussinesLogic.DTOs
{
    public class JourneyDTO
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public Decimal Price { get; set; }
        public List<FlightDTO> Fligths { get; set; }
    }
}
