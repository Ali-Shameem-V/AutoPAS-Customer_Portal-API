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
var connectionStrings = builder.Configuration.GetConnectionString("Assessment");
builder.Services.AddDbContext<AutoPortalDbContext>(options => options.UseMySql(connectionStrings, ServerVersion.AutoDetect(connectionStrings)));
var connectionStrings2 = builder.Configuration.GetConnectionString("Assessment2");
builder.Services.AddDbContext<DemoContext>(options => options.UseMySql(connectionStrings2, ServerVersion.AutoDetect(connectionStrings2)));


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
