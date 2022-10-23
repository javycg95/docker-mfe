namespace Backend.Catalog;

public class DataCatalog
{
    public List<HeroeCatalog> HeroeCatalog { get; internal set; }

    public DataCatalog()
    {
        HeroeCatalog = new List<HeroeCatalog>() 
        {
            new HeroeCatalog
            {
                Universo = new Universo
                {
                    Name = "Marvel",
                },
                Heroes = new List<Heroe>() {
                    new Heroe 
                    {
                        Name = "Lobezno"
                    }
                }
            },
            new HeroeCatalog
            {
                Universo = new Universo
                {
                    Name = "DC",
                },
                Heroes = new List<Heroe>() {
                    new Heroe 
                    {
                        Name = "Superman"
                    }
                }
            }
        };
    }

    public IEnumerable<Universo> GetUniverses()
    {
        return HeroeCatalog
            .Where(x => x.Universo != null)
            .Select(x => x.Universo);
    }

    
    public Universo GetUniverse(string universo)
    {
        return HeroeCatalog
            .Where(x => x.Universo != null)
            .Select(x => x.Universo)
            .Where(x => x.Name?.ToLower() == universo.ToLower())
            .FirstOrDefault();
    }

    public void AddUniverse(string universo)
    {
        var catalog = GetUniverse(universo);

        if (catalog != null)
        {
            return;
        }

        HeroeCatalog.Add(new HeroeCatalog
        {
            Universo = new Universo
            {
                Name = universo
            }
        });
    }

    public void DeleteUniverse(string universo)
    {
        var catalog = HeroeCatalog
            .Where(x => x.Universo != null)
            .Where(x => x.Universo.Name?.ToLower() == universo.ToLower())
            .FirstOrDefault();

        if (catalog == null)
        {
            return;
        }

        HeroeCatalog.Remove(catalog);
    }

    public IEnumerable<Heroe> GetHeroes()
    {
        var result = new List<Heroe>();

        var heroesUniverses = HeroeCatalog
            .Where(x => x.Heroes != null)
            .Select(x => x.Heroes);

        foreach(var heroes in heroesUniverses)
        {
            result.AddRange(heroes);
        }

        return result;
    }

    public IEnumerable<Heroe> GetHeroesByUniverse(string universo)
    {
        var catalog = HeroeCatalog
        .Where(x => x.Universo != null)
        .Where(x => x.Universo.Name?.ToLower() == universo.ToLower())
        .FirstOrDefault();

        if (catalog == null)
        {
            return new List<Heroe>();
        }

        return catalog.Heroes;
    }

    public Heroe? GetHeroeOfUniverse(string universo, string name)
    {
        var heroes = this.GetHeroesByUniverse(universo);

        if (heroes == null)
        {
            return null;
        }

        return heroes?
            .Where(x => x.Name?.ToLower() == name.ToLower())
            .FirstOrDefault();
    }

    public void AddHeroeToUniverse(string universo, string name)
    {
        var catalog = HeroeCatalog
            .Where(x => x.Universo != null)
            .Where(x => x.Universo.Name?.ToLower() == universo.ToLower())
            .FirstOrDefault();

        if (catalog == null)
        {
            throw new BadHttpRequestException("No existe el universo", 404);
        }

        var heroe = catalog.Heroes?
            .Where(x => x.Name?.ToLower() == name.ToLower())
            .FirstOrDefault();

        if (heroe != null)
        {
            return;
        }

        catalog.Heroes.Add(new Heroe
        {
            Name = name
        });
    }

    public void DeleteHeroeToUniverse(string universo, string name)
    {
        var catalog = HeroeCatalog
            .Where(x => x.Universo != null)
            .Where(x => x.Universo.Name?.ToLower() == universo.ToLower())
            .FirstOrDefault();

        if (catalog == null)
        {
            throw new BadHttpRequestException("No existe el universo", 404);
        }

        var heroe = catalog.Heroes
            .Where(x => x.Name?.ToLower() == name.ToLower())
            .FirstOrDefault();

        if (heroe == null)
        {
            throw new BadHttpRequestException("No existe el heroe", 404);
        }

        catalog.Heroes.Remove(heroe);
    }
}

public record HeroeCatalog 
{
    public Universo? Universo { get; set; }

    public IList<Heroe> Heroes {get; set;} = new List<Heroe>();
}

public record Universo
{
    public string Name {get; init;}
}

public record Heroe
{
    public string Name {get; init;}
}
