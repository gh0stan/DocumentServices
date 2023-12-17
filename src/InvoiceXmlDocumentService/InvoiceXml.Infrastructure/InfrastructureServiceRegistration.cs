using InvoiceXml.Application.Contracts.Infrastructure;
using InvoiceXml.Application.Contracts.Persistence;
using InvoiceXml.Application.Models;
using InvoiceXml.Infrastructure.Mail;
using InvoiceXml.Infrastructure.Persistence;
using InvoiceXml.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceXml.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("InvoiceXml")));

            services.AddScoped(typeof(IAsyncRepo<>), typeof(RepositoryBase<>));
            services.AddScoped<IDocumentRepo, DocumentRepo>();
            services.AddScoped<IUserRepo, UserRepo>();

            services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
