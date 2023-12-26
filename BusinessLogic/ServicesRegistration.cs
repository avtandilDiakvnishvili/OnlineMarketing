using BusinessLogic.IRepositories;
using BusinessLogic.MapperProfiles;
using BusinessLogic.Repositories;
using DataAccess.DbContexts;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic
{
    public class ServicesRegistration
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //TODO implements all services
            services.AddScoped<IDistributorRepository, DistributorRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddDbContext<DataContext>();


            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            var mapper = config.CreateMapper();

            services.AddSingleton(mapper);


        }
    }




}
