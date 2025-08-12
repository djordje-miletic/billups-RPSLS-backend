using Billups.RPLS.DAL.Classes;
using Billups.RPLS.DAL.Interfaces;
using Billups.RPSLS.BL.Interfaces;
using Billups.RPSLS.BL.Services;
using Billups.RPSLS.Common;
using Billups.RPSLS.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IChoiceBL, ChoiceBL>();


builder.Services.AddScoped<IChoiceDAL, ChoiceDAL>();


builder.Services.AddDbContext<RPSLSDbContext>(opt => opt.UseInMemoryDatabase("RPSLS Database"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ApplicationExceptionHandler>();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<RPSLSDbContext>();

    DataGenerator.Initialize(services);
}

app.Run();
