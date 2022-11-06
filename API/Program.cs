using API.Services;
using Helpers;
using Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
    options.UseSqlServer(builder.Configuration.GetConnectionString("connectionstring"))
           .UseLazyLoadingProxies();
    //options.UseNpgsql(builder.Configuration.GetConnectionString("connectionstring"))
    //       .UseLazyLoadingProxies();
});

//AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<INote, NoteService>();
builder.Services.AddScoped<TokenService>();

AddSwagger(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Scarlet API V1");
});

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

void AddSwagger(IServiceCollection services)
{
    services.AddSwaggerGen(options =>
    {
        var groupName = "v1";

        options.SwaggerDoc(groupName, new OpenApiInfo
        {
            Title = $"Scarlet {groupName}",
            Version = groupName,
            Description = "Scarlet API",
            Contact = new OpenApiContact
            {
                Name = "Scarlet Project",
                Email = string.Empty,
            }
        });
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer xxxxxxxx\"",
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
    });
}