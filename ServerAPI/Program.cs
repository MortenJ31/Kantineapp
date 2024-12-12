using ServerAPI.Repositories;
using ServerAPI.Services;
using ServerAPI.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;


var builder = WebApplication.CreateBuilder(args);

// Registrer enum-serializer for Status
BsonSerializer.RegisterSerializer(typeof(Status), new EnumSerializer<Status>(BsonType.String));

//Tilføj MongoDB-indstillinger fra appsettings.json
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

//Registrerer MongoClient som singleton
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

//Registrerer IMongoDatabase som singleton
builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return client.GetDatabase(settings.DatabaseName);
});

//Registrerer MongoDbService
builder.Services.AddSingleton<MongoDbService>();

// Repositories
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IOpgaveRepository, OpgaveRepository>();
builder.Services.AddScoped<IBrugerRepository, BrugerRepository>();

/*
// Services
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IOpgaveService, OpgaveService>();
builder.Services.AddScoped<IBrugerService, BrugerService>();
*/

//Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
     {
         options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
     });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
