using ServerAPI.Repositories;
using ServerAPI.Services;
using ServerAPI.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using System.Text.Json.Serialization;
using Core.Models;

var builder = WebApplication.CreateBuilder(args);

// Lige her sørger vi for, at enums gemmes som strenge i databasen
BsonSerializer.RegisterSerializer(typeof(Status), new EnumSerializer<Status>(BsonType.String));
BsonSerializer.RegisterSerializer(typeof(Role), new EnumSerializer<Role>(BsonType.String));

// Hent MongoDB-indstillinger fra appsettings.json
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

// MongoDB bliver sat op som en service her
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
{
    // Henter forbindelsesstrengen fra settings og laver en klient
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    // Her laver vi en forbindelse til selve databasen
    var client = sp.GetRequiredService<IMongoClient>();
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return client.GetDatabase(settings.DatabaseName);
});

// Smider vores MongoDB-service i puljen
builder.Services.AddSingleton<MongoDbService>();

// Her registrerer vi repositories – hver tager sig af deres del af data
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<ITaskItemRepository, TaskItemRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();

// CORS – det her er så vi kan snakke med API’et fra alle steder
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Vi sætter controllers op og tilføjer JSON-konvertering, så enums ser godt ud
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Her smider vi lige Swagger på, så vi får en flot dokumentation til API'et
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Hvis vi er i udvikling, viser vi Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Her kommer den CORS-politik, vi satte før
app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection(); 
app.UseAuthorization(); 

app.MapControllers();

app.Run(); // Og så starter vi hele showet!
