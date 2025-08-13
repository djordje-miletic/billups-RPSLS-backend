using Billups.RPSL.Hubs;
using Billups.RPSLS.BL.Interfaces;
using Billups.RPSLS.BL.Services;
using Billups.RPSLS.Common;
using Billups.RPSLS.DAL.Classes;
using Billups.RPSLS.DAL.Interfaces;
using Billups.RPSLS.DBContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IChoiceBL, ChoiceBL>();

builder.Services.AddScoped<IChoiceDAL, ChoiceDAL>();
builder.Services.AddScoped<IScoreDAL, ScoreDAL>();

builder.Services.AddDbContext<RPSLSDbContext>(opt => opt.UseInMemoryDatabase("RPSLS Database"));

builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials());
});

// In Configure (for Startup.cs) or before app.Run() in Program.cs

var app = builder.Build();

app.UseCors("AllowSpecificOrigin");

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

app.MapHub<GameHub>("/gamehub");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<RPSLSDbContext>();

    DataGenerator.Initialize(services);
}

app.Run();
