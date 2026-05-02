using AutoMapper;
using Microsoft.EntityFrameworkCore;
using National_Park_API.Data;
using National_Park_API.DToMapping;
using National_Park_API.Repository;
using National_Park_API.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var cs = builder.Configuration.GetConnectionString("Constr");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(cs));

builder.Services.AddScoped<INationalParkRepository, NationalParkRepository>();
builder.Services.AddScoped<ITrailRepository, TrailRepository>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();