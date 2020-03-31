using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using WebApiEF.Db;
using WebApiEF.Db.Abstract;
using WebApiEF.Db.Context;
using WebApiEF.Db.Persistence;

namespace Microsoft.Extensions.Hosting
{
    public static class DbInjections
    {
        public static readonly string defaultConnectionString = "Server=(localdb)\\mssqllocaldb;Database=WorkloadsIntro;Trusted_Connection=True;MultipleActiveResultSets=true";

        //This extension is for IHostBuilder so you could add db support on this level
        public static IHostBuilder AddWorkloadDb(this IHostBuilder hostBuilder, Func<IConfiguration, string> GetConnectionString = null)
        {
            hostBuilder.ConfigureServices((hostBuilderContext, serviceCollection) =>
            {
                string connectionString = GetConnectionString != null ?
                    GetConnectionString(hostBuilderContext.Configuration) :
                    defaultConnectionString;

                serviceCollection.AddDbContext<IWorkloadContext, WorkloadContext>(dbContextOptionsBuilder =>
                    dbContextOptionsBuilder.UseSqlServer(connectionString));

                serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
                serviceCollection.AddScoped<IWorkloadService, WorkloadService>();
            });

            return hostBuilder;
        }

        //This extension is for IServiceCollection so you could add db support on this level
        public static IServiceCollection AddWorkloadDb(this IServiceCollection serviceCollection, Func<IConfiguration, string> GetConnectionString = null)
        {
            //Build a temporary service provider to be able to get services from the IOC 
            var sp = serviceCollection.BuildServiceProvider();
            var configuration = sp.GetRequiredService<IConfiguration>();

            string connectionString = GetConnectionString != null ?
                GetConnectionString(configuration) :
                defaultConnectionString;

            serviceCollection.AddDbContext<IWorkloadContext, WorkloadContext>(dbContextOptionsBuilder =>
                dbContextOptionsBuilder.UseSqlServer(connectionString));

            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<IWorkloadService, WorkloadService>();

            return serviceCollection;
        }
    }
}
