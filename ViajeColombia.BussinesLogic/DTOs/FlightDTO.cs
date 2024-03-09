﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViajeColombia.BussinesLogic.DTOs
{
    public class FlightDTO
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public Decimal Price { get; set; }
        public TransportDTO Transport { get; set; }
    }
}