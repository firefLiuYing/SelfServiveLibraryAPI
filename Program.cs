using AutoLibraryAPI.Date;
using AutoLibraryAPI.Models;
using AutoLibraryAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",new OpenApiInfo{Title = "My API" ,Version="v1"});
});

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddDbContext<DataContext>(opt=>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    context.Database.Migrate(); // ??????
}

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
{
    app.MapGet("/debug/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
    {
        var endpoints = endpointSources.SelectMany(es => es.Endpoints);
        return new
        {
            TotalEndpoints = endpoints.Count(),
            Routes = endpoints.Select(e => 
                new { 
                    Pattern = (e as RouteEndpoint)?.RoutePattern?.RawText,
                    Methods = e.Metadata.GetMetadata<HttpMethodMetadata>()?.HttpMethods
                })
        };
    });
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
