var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var catalog = new Backend.Catalog.DataCatalog();

app.MapGet("/", () =>
{
    return Results.Ok(catalog.HeroeCatalog);
})
.WithName("GetAll");

// Universos

app.MapGet("/universos", () =>
{
    return Results.Ok(catalog.GetUniverses());
})
.WithName("GetUniversos");

app.MapGet("/universos/{universo}", (string universo) =>
{
    return Results.Ok(catalog.GetUniverse(universo));
})
.WithName("GetUniverso");

app.MapPut("/universos/{universo}", (string universo) =>
{
    catalog.AddUniverse(universo);
    return Results.NoContent();
})
.WithName("AddUniverso");

app.MapDelete("/universos/{universo}", (string universo) =>
{
    catalog.DeleteUniverse(universo);
    return Results.NoContent();
})
.WithName("DeleteUniverso");

// Heroes

app.MapGet("/universos/heroes", () =>
{
    return Results.Ok(catalog.GetHeroes());
})
.WithName("GetHeroes");

app.MapGet("/universos/{universo}/heroes", (string universo) =>
{
    return Results.Ok(catalog.GetHeroesByUniverse(universo));
})
.WithName("GetHeroesByUniverse");

app.MapGet("/universos/{universo}/heroe/{name}", (string universo, string name) =>
{
    var heroe = catalog.GetHeroeOfUniverse(universo, name);
    if (heroe == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(heroe);
})
.WithName("GetHeroeOfUniverse");

app.MapPut("/universos/{universo}/heroe/{name}", (string universo, string name) =>
{
    catalog.AddHeroeToUniverse(universo, name);
    return Results.NoContent();
})
.WithName("AddHeroeToUniverse");

app.MapDelete("/universos/{universo}/heroe/{name}", (string universo, string name) =>
{
    catalog.DeleteHeroeToUniverse(universo, name);
    return Results.NoContent();
})
.WithName("DeleteHeroeToUniverse");

app.Run("http://localhost:5000");
