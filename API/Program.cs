using API.Interfaces;
using API.Services;
using Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigurationManager configuration = builder.Configuration;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["JWT:ISSUER"],
            ValidAudience = configuration["JWT:AUDIENCE"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["JWT:SECRET_KEY"])
            )
        };
    });

builder.Services.AddControllers();
builder.Services.AddDbContext<ScarletContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("connectionstring"))
           .UseLazyLoadingProxies();
});

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:8100")
        .AllowAnyHeader()
        .WithMethods("GET", "PUT", "POST", "DELETE")
        .AllowCredentials();
});

app.UseAuthorization();

app.MapControllers();

app.Run();