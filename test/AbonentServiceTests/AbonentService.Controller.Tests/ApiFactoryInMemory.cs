using AbonentService.AsyncDataServices;
using AbonentService.Data;
using AbonentService.Interfaces;
using AbonentService.SyncDataServices.Http;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbonentService.Api.Tests
{
    public class ApiFactoryInMemory : WebApplicationFactory<IApiAssemblyMarker>
    {
        private readonly Action<IServiceCollection> _configure;

        public ApiFactoryInMemory(Action<IServiceCollection> configure)
        {
            _configure = configure;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Development");
            
            builder.ConfigureTestServices(services => 
            {
                services.RemoveAll(typeof(IAbonentRepo));
                services.RemoveAll(typeof(IInformalDocDataClient));
                services.RemoveAll(typeof(IMessageBusClient));
                services.RemoveAll(typeof(DataContext));
                services.RemoveAll(typeof(DbContextOptions<DataContext>));

                services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("TestDb"));
                _configure(services);
            });
            
        }
    }
}
