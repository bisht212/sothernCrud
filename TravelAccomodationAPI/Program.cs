
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using System.Threading.RateLimiting;
using TravelAccomodationAPI.BusinessClass.DependencyInjection;
using TravelAccomodationAPI.DataAccessClass.DependencyInjection;
using TravelAccomodationAPI.Shared.DBHelper;
using TravelAccomodationAPI.TokenCreateClass;
using TravelAccomodationAPI.TokenCreateClass.InterFaces;

using TravelAccomodationAPI.CustomeMiddleware;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Add Authentication Token",
        //Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Type = SecuritySchemeType.Http, // Changed to Http for better JWT handling
        BearerFormat = "Jwt",
        Scheme="bearer"
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Name = "Bearer",
                In = ParameterLocation.Header,
                Reference = new OpenApiReference{
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme

                }
        },

            new List<string>()

    } });
});

builder.Services.AddBusinessServices();
builder.Services.AddDataAccess();


//JWT Authentication
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

// Add rate limiting services
builder.Services.AddRateLimiter(options =>
{
    // 1. Define the status code
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    // 2. Define the custom message/response
    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;

        // You can return plain text or a JSON object
        await context.HttpContext.Response.WriteAsJsonAsync(new
        {
            Message = "Too many requests. Please try again later.",
            RetryAfter = "10 seconds",
            StatusCode = 429
        }, cancellationToken: token);
    };

    // 3. Your existing policy
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

builder.Host.
    UseSerilog();

var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionMiddleware>();
//Rate limit middleware
app.UseRateLimiter();

app.UseAuthentication();

app.UseRateLimiter();

app.UseAuthorization();

app.MapControllers();

app.Run();
