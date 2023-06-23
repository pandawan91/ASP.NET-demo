using System.Diagnostics.Metrics;
using ASP.NET_demo.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<ApiContext>(options =>
{
    // connect to postgres
    string connectionString = String.Format("Host={0};Database={1};Username={2};Password={3}",
        builder.Configuration.GetValue<string>("POSTGRES_HOST"),
        builder.Configuration.GetValue<string>("POSTGRES_DB"),
        builder.Configuration.GetValue<string>("POSTGRES_USER"),
        builder.Configuration.GetValue<string>("POSTGRES_PASSWORD"));
    options.UseNpgsql(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

    var context = services.GetService<ApiContext>()
        ?? throw new Exception("Can't Find ApiContext");
    context.Database.Migrate();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

