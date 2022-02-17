using Hipster.Api;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApiDocument(options => {
    options.Title = "Hipster.Api";
});
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
});
builder.Services.AddCors();

var app = builder.Build();
app.UseRouting();
app.UseCors(x => 
{
    x.AllowAnyOrigin();
    x.AllowAnyMethod();
    x.AllowAnyHeader();
});
app.UseOpenApi();
app.UseSwaggerUi3();

app.MapControllers();

app.Run();