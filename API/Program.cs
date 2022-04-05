using API.Interfaces;
using API.Services;
using Helpers;
using Microsoft.EntityFrameworkCore;
using Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

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