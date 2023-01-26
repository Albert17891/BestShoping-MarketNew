using MarketPlace.Core.Entities;
using MarketPlace.Core.Interfaces;
using MarketPlace.Core.Interfaces.Account;
using MarketPlace.Core.Services;
using MarketPlace.Infastructure.Data;
using MarketPlace.Infastructure.Data.Account;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MarketPlace.Infastructure;

public static class ServiceCollectionExtensions
{
    private static AccessTokenConfiguration _options;

    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AccessTokenConfiguration>(configuration.GetSection(nameof(AccessTokenConfiguration)));

        var serviceProvider = services.BuildServiceProvider();
        _options = serviceProvider.GetRequiredService<IOptions<AccessTokenConfiguration>>().Value;

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = _options.Issuer,
                    ValidAudience = _options.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });

        services.AddScoped<IAuthenticationCreator, AuthenticationCreator>();
        services.AddScoped<IUserAuthentication, UserAuthentication>();
        services.AddScoped<IProductService, ProductServices>();

        services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("ApplicationConnection")));

        services.AddIdentity<AppUser, Microsoft.AspNetCore.Identity.IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

        return services;
    }
}
