
namespace PokemonAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

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

            var pokemons = new List<Pokemon> {

            new Pokemon {Id=1, Name = "Bulbasaur", Type="Grass"},
                new Pokemon {Id=2, Name = "Ivysaur", Type="Grass"},
                new Pokemon {Id=3, Name = "Venosaur", Type="Grass"},
                new Pokemon {Id=4, Name = "Charmander", Type="Fire"}
            };

            app.MapPost("/pokemon", (Pokemon pokemon) =>
            {
                pokemons.Add(pokemon);
                return Results.Ok(pokemon);
            });

            app.MapGet("/pokemons", () =>
            {
                return Results.Ok(pokemons);
            });

            app.MapGet("/pokemon/{id}", (int id) =>
            {
                var pokemon = pokemons.Find(p => p.Id == id);

                if (pokemon == null) {
                    return Results.NotFound("Pokemon with this id not found"); }
                return Results.Ok(pokemon);
            });

            app.MapPut("/pokemon/{id}", (Pokemon updatePokemon, int id) => {         
            var pokemon = pokemons.Find(p => p.Id == id);
            if(pokemon == null)
            {
                return Results.NotFound("Pokemon with this id not found");
            }
                //pokemon.Id = updatePokemon.Id;
                //pokemon.Name = updatePokemon.Name;
                //pokemon.Type = updatePokemon.Type;

                pokemons[id-1] = updatePokemon;

                return Results.Ok(pokemon);
             });

            app.MapDelete("/pokemon/{id}", (int id) =>
            {
                var pokemonToRemove = pokemons.Find(p => p.Id == id);
                if (pokemonToRemove == null)
                {
                    return Results.NotFound("Pokemon with this id not found");
                }

                pokemons.Remove(pokemonToRemove);
                return Results.Ok("Pokemon was removed successfully");
            });


            app.Run();
        }
    }
}
