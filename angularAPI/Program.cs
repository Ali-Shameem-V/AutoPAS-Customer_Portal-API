using angularAPI.Model;
using angularAPI.Services;
using angularAPI.Services.Interface;
using AutoPortal.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var auto_portal = builder.Configuration.GetConnectionString("auto_portal");
builder.Services.AddDbContext<AutoPortalDbContext>(options => options.UseMySql(auto_portal, ServerVersion.AutoDetect(auto_portal)));
var auto_pas = builder.Configuration.GetConnectionString("auto_pas");
builder.Services.AddDbContext<DemoContext>(options => options.UseMySql(auto_pas, ServerVersion.AutoDetect(auto_pas)));


builder.Services.AddScoped<IPolicy, PolicyRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());


app.MapControllers();

app.Run();
