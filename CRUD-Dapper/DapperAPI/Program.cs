using BusinessLayer.Interface;
using BusinessLayer.Service;
using Confluent.Kafka;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<AppDbContext>();
//builder.Services.AddScoped<IStudentBL, StudentBL>();
builder.Services.AddScoped<IStudentRL, StudentRL>();
builder.Services.AddScoped<IStudentBL, StudentBL>();

// Register Kafka consumer config
builder.Services.AddSingleton<ConsumerConfig>(sp =>
{
    // Configure Kafka consumer properties
    return new ConsumerConfig
    {
        BootstrapServers = "localhost:9092", // Kafka broker address
        GroupId = "my-consumer-group", // Consumer group ID
        AutoOffsetReset = AutoOffsetReset.Earliest // Reset offset to beginning
    };
});

// Register Kafka consumer
builder.Services.AddSingleton(sp =>
{
    // Retrieve the registered ConsumerConfig service
    var consumerConfig = sp.GetRequiredService<ConsumerConfig>();

    // Build the consumer using the retrieved config
    return new ConsumerBuilder<string, string>(consumerConfig).Build();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var consumer = new ConsumerBuilder<string, string>(app.Services.GetRequiredService<ConsumerConfig>()).Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
