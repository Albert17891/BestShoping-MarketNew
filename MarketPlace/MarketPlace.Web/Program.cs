using FluentValidation.AspNetCore;
using MarketPlace.Core.Behaviors;
using MarketPlace.Core.Handlers.QueryHandlers;
using MarketPlace.Infastructure;
using MarketPlace.Web.Infastructure.Middleware;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Host.UseSerilog();

var configuration = builder.Configuration;


var logConfiguration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAppServices(configuration);

builder.Services.AddMediatR(typeof(GetProductsHandler).Assembly)
                .AddScoped(typeof(IPipelineBehavior<,>),typeof(LoggingPipelineBehavior<,>));

builder.Services.AddFluentValidation(conf =>
{
    conf.RegisterValidatorsFromAssemblyContaining(typeof(Program));

});


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
})
           .AddJwtBearer(opt =>
           {
               opt.TokenValidationParameters = new TokenValidationParameters()
               {
                   ValidIssuer = "localhost",
                   ValidAudience = "localhost",
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is my Secret Key Not to use in Production Time")),
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = false,
                   ValidateIssuerSigningKey = true
               };
           });
//}).AddCookie("cookie")
//.AddOAuth("github", o =>
//{
//    o.SignInScheme = "cookie";
//    o.ClientId = "ef48f886f6510bea43a3";
//    o.ClientSecret = "9e0d022c58e8bb6299ce77d9ec1d746c084ba13c";

//    o.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
//    o.TokenEndpoint = "https://github.com/login/oauth/access_token";

//    o.CallbackPath = "/oauth/github-cp";

//    o.SaveTokens = true;

//    o.UserInformationEndpoint = "https://api.github.com.user";


//});

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandler>();
app.UseMiddleware<LoggingMiddleware>();

app.UseSerilogRequestLogging();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await Seeding.SeedUserAsync(app.Services);

app.Run();
