using GestaoGastos.Application.Services.Implementations;
using GestaoGastos.Application.Services.Interfaces;
using GestaoGastos.Domain.Interfaces;
using GestaoGastos.Infra.Context;
using GestaoGastos.Infra.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GestaoGastos.Infra.IoC
{
    public static class InjecaoDependencia
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();

            services.AddScoped<IAutenticacaoService, AutenticacaoService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ICategoriaService, CategoriaService>();

            return services;
        }
    }
}
