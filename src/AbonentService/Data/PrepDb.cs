using AbonentService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AbonentService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope()) 
            { 
                SeedData(serviceScope.ServiceProvider.GetService<DataContext>());
            }
        }

        private static void SeedData(DataContext context) 
        {
            Console.WriteLine("DEBUG. Attempting to apply migrations...");

            try
            {
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR. Could not run migrations {ex.Message}");
            }

            if (!context.Abonents.Any())
            {
                Console.WriteLine("DEBUG. Seeding Sample Abonent Data...");

                context.Abonents.AddRange(
                    new Abonent { Name = "Test_Abonent_1" },
                    new Abonent { Name = "Test_Abonent_2" },
                    new Abonent { Name = "Test_Abonent_3" }
                    );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("DEBUG. We already have Abonent data.");
            }

            if (!context.Addresses.Any())
            {
                Console.WriteLine("DEBUG. Seeding Sample Address Data...");
                
                context.Addresses.AddRange(
                        new Address { AbonentId = 1, Name = "abonent_1_address_1", PostalCode = "111222", Country = "Serbia", City = "Belgrade", Street = "ttt", Building = "1" },
                        new Address { AbonentId = 1, Name = "abonent_1_address_2", PostalCode = "111333", Country = "Serbia", City = "Belgrade" },
                        new Address { AbonentId = 1, Name = "abonent_1_address_3", PostalCode = "111444", Country = "Serbia" },
                        new Address { AbonentId = 2, Name = "abonent_2_address_1", PostalCode = "222555", Country = "Serbia" },
                        new Address { AbonentId = 3, Name = "abonent_3_address_1", PostalCode = "333567", Country = "Serbia", City = "Belgrade", Street = "aaa", Building = "13" }
                    );
                
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("DEBUG. We already have Address data.");
            }
        }
    }
}
