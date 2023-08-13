using Aqar.Infrastructure.DataLayer;
using Aqar.Data.Data;
using Serilog;
using Aqar.Extenstions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
//builder.Services.AddDependencyInjection();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
 

 
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDataLayer(configuration)
              .AddAuthLayer(configuration);
builder.Services.AddDependencyInjection();

builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Insert Bearer token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    //Valided filled

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                 Id = "Bearer"
                            },

                             Scheme = "oauth2",
                            Name= "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(c => c.AddPolicy("CorsPolicy", p => p.AllowAnyOrigin().AllowAnyHeader()
.AllowAnyMethod()));

var app = builder.Build();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI();
Log.Logger = new LoggerConfiguration()
                .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Minute)
                .CreateLogger();
var environment = app.Services.GetRequiredService<IWebHostEnvironment>();
app.ConfigureExceptionHandler(Log.Logger, environment);
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();
app.MapFallbackToFile("index.html");

await app.Initialize();

app.Run();
