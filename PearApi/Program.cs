using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PearApi.Authentication;
using PearApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add database context

builder.Services.AddDbContext<PearContext>(opt => opt.UseNpgsql("Host=localhost;Database=pg-docker;Username=postgres;Password=docker"));

// Needed to prevent infinite recurssion in json generation
builder.Services.AddControllers()
   .AddJsonOptions(o =>
   o.JsonSerializerOptions.ReferenceHandler =
    // System.Text.Json.Serialization.ReferenceHandler.Preserve); // Adds $ref references in json
    System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles); // Sets references to null in  json)

// configure JWT authentication
var jwtTokenConfig = builder.Configuration.GetSection("jwtTokenConfig").Get<JwtTokenConfig>()!;
builder.Services.AddSingleton(jwtTokenConfig!);
builder.Services.AddScoped<IAuthService, AuthService>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(jwtConfig =>
    {
       jwtConfig.RequireHttpsMetadata = true;
       jwtConfig.SaveToken = true;
       jwtConfig.TokenValidationParameters = new TokenValidationParameters
       {
          ValidateIssuer = true,
          ValidIssuer = jwtTokenConfig.Issuer,
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret!)),
          ValidAudience = jwtTokenConfig.Audience,
          ValidateAudience = true,
          ValidateLifetime = true,
          ClockSkew = TimeSpan.FromMinutes(1)
       };
    });
builder.Services.AddScoped<IJwtAuthManager, JwtAuthManager>();



builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
   c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

   c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
   {
      Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
      Name = "Authorization",
      In = ParameterLocation.Header,
      Type = SecuritySchemeType.ApiKey,
      Scheme = JwtBearerDefaults.AuthenticationScheme
   });

   c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            Array.Empty<string>()
        }
    });
});

// Register Seeder
builder.Services.AddTransient<Seeder>();

var app = builder.Build();


// Seed DB (dotnet run seed)
if (args.Length == 1 && args[0].ToLower() == "seed")
{
   Console.WriteLine("Seeding DB");
   Seed(app);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseCors(builder => builder
     .AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

// Seed Data (Calls the Seeder)
void Seed(IHost app)
{
   var scopedFactory = app.Services.GetService<IServiceScopeFactory>()!;

   using (var scope = scopedFactory.CreateScope())
   {
      var service = scope.ServiceProvider.GetService<Seeder>()!;
      service.Seed();
   }
}