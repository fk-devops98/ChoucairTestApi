using ChoucairTest.Api.Filters;
using ChoucairTest.Domain.Entities;
using ChoucairTest.Infrastructure.Context;
using ChoucairTest.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

string cnnString = config.GetConnectionString("database");
string applicationDllName = "ChoucairTest.Application";
string policyCorsName = "Autorizar";

bool useDatabaseInMemory = config.GetValue("DatabaseSettings:UseDatabaseInMemory", false);
string databaseInMemoryName = config.GetValue("DatabaseSettings:DatabaseInMemoryName", string.Empty);

string[] acceptedMethodsHttp = { "GET", "PUT", "POST", "DELETE" };
string[] acceptedOrigins = { "http://localhost:4200" };

builder.Services.AddControllers(opts => {
    opts.Filters.Add(typeof(AppExceptionFilterAttribute));
    opts.Filters.Add(new AuthorizeFilter());
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(policyCorsName, builder =>
    {
        builder
            .AllowAnyHeader()
            .WithMethods(acceptedMethodsHttp)
            .WithOrigins(acceptedOrigins)
            .SetIsOriginAllowedToAllowWildcardSubdomains();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(Assembly.Load(applicationDllName), typeof(Program).Assembly);
builder.Services.AddAutoMapper(Assembly.Load(applicationDllName));

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddDbContext<PersistenceContext>(opt =>
{
    if (useDatabaseInMemory)
    {
        opt.UseInMemoryDatabase(databaseInMemoryName);   
    }

    if (!useDatabaseInMemory)
    {
        opt.UseSqlServer(cnnString);
    }
});

builder.Services
    .AddIdentity<Usuario, IdentityRole>(
        options =>
        {
            options.User.RequireUniqueEmail = true;

            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 5;
            options.Password.RequiredUniqueChars = 1;
        })
    .AddEntityFrameworkStores<PersistenceContext>()
    .AddDefaultTokenProviders();

builder.Services
    .AddHttpContextAccessor()
    .AddAuthorization()
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddPersistence(config).AddDomainServices();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "ChoucairTest Api", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Inserte el token JWT en este formato: Bearer {token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
});

Log.Logger = new LoggerConfiguration()
    .Enrich
    .FromLogContext()
    .WriteTo
    .Console()
    .CreateLogger();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChoucairTest Api"));

app.UseHttpsRedirection();
app.UseCors(policyCorsName);

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();