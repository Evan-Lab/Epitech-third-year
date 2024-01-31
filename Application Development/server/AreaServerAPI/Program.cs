using AreaServer.Core.Models;
using AreaServer.Core.Settings;
using AreaServer.Infrastructure.Data;
using AreaServerAPI.Controllers;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

string corssettings = "corssettings";

builder.Services.AddCors(p => p.AddPolicy(corssettings, builder =>
{
    builder.SetIsOriginAllowedToAllowWildcardSubdomains()
           .WithOrigins(
                        "http://localhost:8080",
                        "http://localhost:8081",
                        "http://localhost:8082"
                        )
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .AllowAnyHeader();
}));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase")));

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddScoped<AppDbContext>();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));

builder.Services.AddHttpClient();
builder.Services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();

//---

// Adding Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
  {
      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:SecretKey"])),
          ValidateIssuer = false,
          ValidateAudience = false
      };
  });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireJWT", policy => policy.RequireAuthenticatedUser());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//---

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.EnableAnnotations();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
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


builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new DefaultInfrastructureModule());
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));

//app.UseHttpsRedirection();

app.UseCors(corssettings);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<AppDbContext>();

        //---

        //Do not use this, it will delete the entire database, you might only want to delete the schema+tables of this microservice!
        /* //context.Database.EnsureDeleted();*/

        //---

        context.Database.SetCommandTimeout(600);
        context.Database.Migrate();

        //create the database and/or tables when they don't exist
        context.Database.EnsureCreated();

        //to automatically populate datas in the tables
        //but no need to do that here, it can be done with the GenerateAllDatasController with swagger
        await SeedData.Initialize(services);

        //-------

    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
    }
}


app.Run();
