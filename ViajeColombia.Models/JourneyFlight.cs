using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViajeColombia.Models
{
    public class JourneyFlight
    {
        public int Id { get; set; }
        public int IdJourney { get; set; }
        [ForeignKey("IdJourney")]
        public Journey Journey { get; set; }
        public int IdFligth { get; set; }
        [ForeignKey("IdFligth")]
        public Flight Fligth{ get; set; }
    }
}
