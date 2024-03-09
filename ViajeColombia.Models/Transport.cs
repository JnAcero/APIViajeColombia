using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViajeColombia.Models
{
    public class Transport
    {
        [Key]
        public int Id { get; set; }
        public string FlightCarrier { get; set; }
        public string FlightNumber { get; set; }
        public List<Flight> Fligths { get; set; }
    }
}
