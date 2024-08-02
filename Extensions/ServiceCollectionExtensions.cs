using EbookStore.Validation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EbookStore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AutorValidator>());

            services.AddControllersWithViews()
              .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CategoriaValidator>());

            services.AddControllersWithViews()
              .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ClienteValidator>());

            services.AddControllersWithViews()
              .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LivroValidator>());


            return services;
        }

    }
}
