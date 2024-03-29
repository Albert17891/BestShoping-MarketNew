﻿using MarketPlace.Core.Entities;
using MarketPlace.Core.Interfaces.Account;
using MarketPlace.Core.Interfaces.DapperInterfaces;
using MarketPlace.Core.Interfaces.Repository;
using MarketPlace.Core.Interfaces.Services;
using MarketPlace.Core.Services;
using MarketPlace.Infastructure.Data;
using MarketPlace.Infastructure.Data.Account;
using MarketPlace.Infastructure.Data.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MarketPlace.Infastructure;

public static class ServiceCollectionExtensions
{
    private static AccessTokenConfiguration _options;

    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AccessTokenConfiguration>(configuration.GetSection(nameof(AccessTokenConfiguration)));

        var serviceProvider = services.BuildServiceProvider();
        //_options = serviceProvider.GetRequiredService<IOptions<AccessTokenConfiguration>>().Value;

        //services.AddAuthentication(options =>
        //{
        //    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //    options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
        //})
        //    .AddJwtBearer(opt =>
        //    {
        //        opt.TokenValidationParameters = new TokenValidationParameters()
        //        {
        //            ValidIssuer = _options.Issuer,
        //            ValidAudience = _options.Audience,
        //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key)),
        //            ValidateIssuer = true,
        //            ValidateAudience = true,
        //            ValidateLifetime = false,
        //            ValidateIssuerSigningKey = true
        //        };
        //    });

        var logger = serviceProvider.GetService<ILogger<AppLogClass>>();
        services.AddSingleton(typeof(ILogger), logger);

        services.AddScoped<IAuthenticationCreator, AuthenticationCreator>();
        services.AddScoped<IUserAuthentication, UserAuthentication>();       
        services.AddScoped<ICardService, CardService>();

        services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("ApplicationConnection")));

        services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();


        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<IVaucerService, VaucerService>();
        services.AddScoped<IUserAccountService, UserAccountService>();
        services.AddScoped<IDiscountService, DiscountService>();

        return services;
    }
}
