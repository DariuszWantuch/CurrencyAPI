using CurrencyAPI.Data;
using CurrencyAPI.Data.Repositories;
using CurrencyAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IExchangeService, ExchangeService>();
builder.Services.AddScoped<IExchangeRepository, ExchangeRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy",
       builder => builder
          .SetIsOriginAllowedToAllowWildcardSubdomains()
          .WithOrigins("http://localhost:5173", "https://dariuszwantuch.github.io")
          .AllowAnyMethod()
          .AllowCredentials()
          .AllowAnyHeader()
          .Build()
       );
});


// Configure EF Core with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CurrencyAPI v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseCors("MyCorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
