using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViajeColombia.BussinesLogic.Utility;
using ViajeColombia.DataAccess.DBContext;

namespace ViajeColombia.Tests
{
    public class BaseTest
    {
        protected ViajeColombiaContext BuildContext(string DBname)
        {
            var options = new DbContextOptionsBuilder<ViajeColombiaContext>()
                .UseInMemoryDatabase(DBname).Options;

            var dbContext = new ViajeColombiaContext(options);
            return dbContext;
        }

        protected IMapper ConfigureAutoMapper()
        {
            var config = new MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapperProfile());
            });

            return config.CreateMapper();
        }
    }
}
