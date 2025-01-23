
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

            app.MapGet("/pokemons", () =>
            {
                return Results.Ok(pokemons);
            });
                     
            app.Run();
        }
    }
}
