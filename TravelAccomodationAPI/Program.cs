using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;
using System.Threading.RateLimiting;
using TravelAccomodationAPI.BusinessClass.DependencyInjection;
using TravelAccomodationAPI.CustomeMiddleware;
using TravelAccomodationAPI.CustomFilters;
using TravelAccomodationAPI.DataAccessClass.DependencyInjection;
using TravelAccomodationAPI.Shared.DBHelper;
using TravelAccomodationAPI.TokenCreateClass;
using TravelAccomodationAPI.TokenCreateClass.InterFaces;

var builder = WebApplication.CreateBuilder(args);

try
{
    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            In = ParameterLocation.Header,
            Description = "Add Authentication Token",
            Type = SecuritySchemeType.Http,
            BearerFormat = "Jwt",
            Scheme = "bearer"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                },
                new List<string>()
            }
        });
    });

    builder.Services.AddBusinessServices();
    builder.Services.AddDataAccess();

    // JWT Authentication
    builder.Services.AddAuthentication("Bearer")
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
        });

    // Rate Limiting
    builder.Services.AddRateLimiter(options =>
    {
        options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
        options.OnRejected = async (context, token) =>
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            await context.HttpContext.Response.WriteAsJsonAsync(new
            {
                Message = "Too many requests. Please try again later.",
                RetryAfter = "10 seconds",
                StatusCode = 429
            }, cancellationToken: token);
        };

        options.AddFixedWindowLimiter("fixed", opt =>
        {
            opt.PermitLimit = 2;
            opt.Window = TimeSpan.FromSeconds(10);
            opt.QueueLimit = 0;
        });
    });

    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();

    builder.Host.UseSerilog();

    builder.Services.AddValidatorsFromAssemblyContaining<Program>();
    builder.Services.AddScoped<ValidationFilter>();

    builder.Services.AddControllers(options =>
    {
        options.Filters.Add<ValidationFilter>(); // GLOBAL
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseMiddleware<GlobalExceptionMiddleware>();
    app.UseRateLimiter();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    // Log the exception and exit gracefully
    Log.Fatal(ex, "Application startup failed");
}
finally
{
    Log.CloseAndFlush();
}
