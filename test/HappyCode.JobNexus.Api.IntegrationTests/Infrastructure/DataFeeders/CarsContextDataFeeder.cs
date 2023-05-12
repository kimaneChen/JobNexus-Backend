using HappyCode.JobNexus.Core;
using HappyCode.JobNexus.Core.Models;

namespace HappyCode.JobNexus.Api.IntegrationTests.Infrastructure.DataFeeders
{
    public class CarsContextDataFeeder
    {
        public static void Feed(CarsContext dbContext)
        {
            var cars = new[]
            {
                new Car
                {
                    Plate = "DW 12345",
                    Model = "Toyota Avensis",
                },
                new Car
                {
                    Plate = "SB 98765",
                    Model = "Mercedes-Benz",
                },
            };
            dbContext.Cars.AddRange(cars);
            dbContext.SaveChanges();
        }
    }
}
