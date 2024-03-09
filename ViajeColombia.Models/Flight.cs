using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViajeColombia.Models
{
    public class Flight
    {
        [Key]
        public int Id { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public Decimal Price { get; set; }
        public int IdTransport { get; set; }
        [ForeignKey("IdTransport")]
        public Transport Transport { get; set; }
        public List<JourneyFlight> JourneyFlights { get; set; }
    }
}
