using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViajeColombia.Models
{
    public class Journey
    {
        [Key]
        public int Id { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public Decimal Price { get; set; }
        public List<JourneyFlight> JourneyFlights { get; set; }

    }
}
