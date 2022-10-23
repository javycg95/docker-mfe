using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddRefitClient<IBackendService>()
    .ConfigureHttpClient((provider, httpclient) =>
    {
        httpclient.BaseAddress = new Uri("http://localhost:5000");
    });

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

app.MapGet("/", () =>
{
    var service = app.Services.GetRequiredService<IBackendService>();
    return service.GetAll();
})
.WithName("GetAll");

// Universos

app.MapGet("/universos", () =>
{
    var service = app.Services.GetRequiredService<IBackendService>();
    return service.GetUniversos();
})
.WithName("GetUniversos");

app.MapGet("/universos/{universo}", (string universo) =>
{
    var service = app.Services.GetRequiredService<IBackendService>();
    return service.GetUniverso(universo);
})
.WithName("GetUniverso");

app.MapPut("/universos/{universo}", (string universo) =>
{
    var service = app.Services.GetRequiredService<IBackendService>();
    return service.AddUniverso(universo);
})
.WithName("AddUniverso");

app.MapDelete("/universos/{universo}", (string universo) =>
{
    var service = app.Services.GetRequiredService<IBackendService>();
    return service.DeleteUniverso(universo);
})
.WithName("DeleteUniverso");

// Heroes

app.MapGet("/universos/heroes", () =>
{
    var service = app.Services.GetRequiredService<IBackendService>();
    return service.GetHeroes();
})
.WithName("GetHeroes");

app.MapGet("/universos/{universo}/heroes", (string universo) =>
{
    var service = app.Services.GetRequiredService<IBackendService>();
    return service.GetHeroesByUniverse(universo);
})
.WithName("GetHeroesByUniverse");

app.MapGet("/universos/{universo}/heroe/{name}", (string universo, string name) =>
{
    var service = app.Services.GetRequiredService<IBackendService>();
    return service.GetHeroeOfUniverse(universo, name);
})
.WithName("GetHeroeOfUniverse");

app.MapPut("/universos/{universo}/heroe/{name}", (string universo, string name) =>
{
    var service = app.Services.GetRequiredService<IBackendService>();
    return service.AddHeroeToUniverse(universo, name);
})
.WithName("AddHeroeToUniverse");

app.MapDelete("/universos/{universo}/heroe/{name}", (string universo, string name) =>
{
    var service = app.Services.GetRequiredService<IBackendService>();
    return service.DeleteHeroeToUniverse(universo, name);
})
.WithName("DeleteHeroeToUniverse");

app.Run("http://localhost:5001");

internal interface IBackendService
{
    [Get("")]
    Task<IEnumerable<HeroeCatalog>?> GetAll();

    // Universos
    [Get("/universos")]
    Task<IEnumerable<Universo>?> GetUniversos();

    [Get("/universos/{universo}")]
    Task<Universo?> GetUniverso(string universo);

    [Put("/universos/{universo}")]
    Task AddUniverso(string universo);

    [Delete("/universos/{universo}")]
    Task DeleteUniverso(string universo);

    // Heroes
    [Get("/universos/heroes")]
    Task<IEnumerable<Heroe>?> GetHeroes();

    [Get("/universos/{universo}/heroes")]
    Task<IEnumerable<Heroe>?> GetHeroesByUniverse(string universo);

    [Get("/universos/{universo}/heroe/{name}")]
    Task<Heroe> GetHeroeOfUniverse(string universo, string name);

    [Put("/universos/{universo}/heroe/{name}")]
    Task AddHeroeToUniverse(string universo, string name);

    [Delete("/universos/{universo}/heroe/{name}")]
    Task DeleteHeroeToUniverse(string universo, string name);

}

internal record HeroeCatalog 
{
    public Universo? Universo { get; set; }

    public IList<Heroe>? Heroes {get; set;}
}

internal record Universo
{
    public string Name {get; init;}
}

internal record Heroe
{
    public string Name {get; init;}
}