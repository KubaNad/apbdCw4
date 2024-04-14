namespace WebApplication1.Animals;

public static class Configuration
{
    public static void RegisterEndpointsForAnimals(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/v1/animals", (IAnimalsService service) =>
        {
            // var service = new AnimalService();
            // var result = service.GetAnimals();
            return TypedResults.Ok(service.GetAnimals());
        });

        app.MapGet("/api/v1/animals/{id:int}", (int id) => { TypedResults.Ok();});
        
        app.MapPost("/api/v1/animals", (Animal newAnimal) => { TypedResults.Ok();});

    }
}