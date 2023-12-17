using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace InvoiceXml.Infrastructure.Persistence.EntityConfiguration
{
    public class EFConfigurationProvider : ConfigurationProvider
    {
        Action<DbContextOptionsBuilder> OptionsAction { get; }

        public EFConfigurationProvider(Action<DbContextOptionsBuilder> optionsAction)
        {
            OptionsAction = optionsAction;
        }

        public override void Load()
        {
            var builder = new DbContextOptionsBuilder<DataContext>();

            OptionsAction(builder);

            using (var dbContext = new DataContext(builder.Options))
            {
                if (dbContext == null || dbContext.AppSettings == null)
                {
                    throw new Exception("Null DB context");
                }

                Data = dbContext.AppSettings.ToDictionary(c => c.Key, c => c.Value);
            }
        }
    }
}