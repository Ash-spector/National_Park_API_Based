using Microsoft.EntityFrameworkCore;
using National_Park_API.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Strong validation (don’t trust config blindly)
var cs = builder.Configuration.GetConnectionString("Constr");

//if (string.IsNullOrEmpty(cs))
//{
//    throw new Exception("Connection string 'Constr' is NULL. Check appsettings.json");
//}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(cs));

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();