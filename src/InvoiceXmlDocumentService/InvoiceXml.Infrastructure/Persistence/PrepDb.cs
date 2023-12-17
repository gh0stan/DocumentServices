using InvoiceXml.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceXml.Infrastructure.Persistence
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
                    new Abonent { Name = "Test_Abonent_1", ExternalId = 1, CreatedDate = DateTime.Now },
                    new Abonent { Name = "Test_Abonent_2", ExternalId = 2, CreatedDate = DateTime.Now },
                    new Abonent { Name = "Test_Abonent_3", ExternalId = 3, CreatedDate = DateTime.Now }
                    );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("DEBUG. We already have Abonent data.");
            }

            if (!context.Users.Any())
            {
                Console.WriteLine("DEBUG. Seeding Sample Users Data...");

                context.Users.AddRange(
                        new User { FirstName = "OneF", LastName = "OneL",  AbonentId = 1, ExternalId = 1, 
                            Email = "1@oneone.com", UserName = "one", CreatedDate = DateTime.Now },
                        new User { FirstName = "TwoF", LastName = "TwoL", AbonentId = 2, ExternalId = 2, 
                            Email = "2@twotwo.com", UserName = "two", CreatedDate = DateTime.Now }
                    );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("DEBUG. We already have Users data.");
            }

            if (!context.Documents.Any())
            {
                Console.WriteLine("DEBUG. Seeding Sample Document Data...");

                context.Documents.Add(
                        new Document {  
                            Guid = Guid.NewGuid().ToString(), 
                            DocumentXml = "<Document><FileName>InvoiceN1.xml</FileName><Title>Invoice Title</Title><Total>100.31</Total></Document>",
                            FileName = "InvoiceN1.xml",
                            CreatedBy = 1, 
                            DocumentDate = DateTime.Now, 
                            InvoiceTotal = 100.31M, 
                            SenderAbonentId = 1, 
                            ReceiverAbonentId = 2, 
                            Title = "Invoice Title" 
                        });

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("DEBUG. We already have Document data.");
            }
        }
    }
}
