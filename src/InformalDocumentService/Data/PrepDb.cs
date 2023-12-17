using Microsoft.EntityFrameworkCore;
using InformalDocumentService.Data.Models;
using InformalDocumentService.Data.Repositories;
using InformalDocumentService.SyncDataServices.Grpc;

namespace InformalDocumentService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope()) 
            {
                SeedData(serviceScope.ServiceProvider.GetService<DataContext>());

                Console.WriteLine("DEBUG. Getting abonents by Grpc...");
                var grpcClient = serviceScope.ServiceProvider.GetService<IAbonentDataClient>();
                var abonents = grpcClient.GetAllAbonents();

                if (abonents != null)
                {
                    SeedMissingAbonentData(serviceScope.ServiceProvider.GetService<IDocumentRepo>(), abonents);
                }
            }
        }

        private static void SeedMissingAbonentData(IDocumentRepo documentRepo, IEnumerable<Abonent> abonents)
        {
            Console.WriteLine("DEBUG. Seeding new abonents...");

            foreach(var abonent in abonents) 
            { 
                if(!documentRepo.AbonentExists(abonent.ExternalId)) 
                { 
                    documentRepo.CreateAbonent(abonent);
                }
            }

            documentRepo.SaveChanges();
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

            if (!context.Documents.Any())
            {
                Console.WriteLine("DEBUG. Seeding sample Document data...");

                context.Documents.AddRange(
                        new Document { DocumentDate = DateTime.Now, Guid = Guid.NewGuid().ToString(), SenderId = 1, ReceiverId = 2, Title = "Hello World" },
                        new Document { DocumentDate = DateTime.Now.AddDays(-1), Guid = Guid.NewGuid().ToString(), SenderId = 1, ReceiverId = 2, Title = "Invoice 1" },
                        new Document { DocumentDate = DateTime.Now.AddDays(-2), Guid = Guid.NewGuid().ToString(), SenderId = 1, ReceiverId = 3, Title = "Invoice 2" },
                        new Document { DocumentDate = DateTime.Now.AddMonths(-1), Guid = Guid.NewGuid().ToString(), SenderId = 2, ReceiverId = 1, Title = "Invoice ABC 1" },
                        new Document { DocumentDate = DateTime.Now.AddMonths(-1), Guid = Guid.NewGuid().ToString(), SenderId = 2, ReceiverId = 2, Title = "Random Name 1" },
                        new Document { DocumentDate = DateTime.Now.AddMonths(-2), Guid = Guid.NewGuid().ToString(), SenderId = 3, ReceiverId = 1, Title = "Random Name 1" }
                    );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("DEBUG. We already have Document data.");
            }
        }
    }
}
