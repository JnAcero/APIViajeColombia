using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ViajeColombia.Models;

namespace ViajeColombia.DataAccess.DBContext
{
    public class ViajeColombiaContext: DbContext
    {
        public ViajeColombiaContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Flight> Fligths { get; set; }
        public DbSet<Journey> Journeys { get; set; }
        public DbSet<JourneyFlight> JourneyFlights { get; set; }
        public DbSet<Transport> Transports { get; set; }

    }
}
