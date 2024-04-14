namespace WebApplication1.Animals;

public static class ConfigurationForAnilmas
{
    public static void RegisterEndpointsForAnimals(this IEndpointRouteBuilder app)
    {
        app.MapGet("/animals", (IAnimalsService service) => service.GetAnimals());
        app.MapGet("/animals/{id}", (int id, IAnimalsService service) => 
            service.GetAnimalById(id) is Animal animal ? Results.Ok(animal) : Results.NotFound());
        app.MapPost("/animals", (Animal animal, IAnimalsService service) => 
            Results.Created($"/animals/{animal.Id}", service.AddAnimal(animal)));
        app.MapPut("/animals/{id}", (int id, Animal updateAnimal, IAnimalsService service) => 
            service.UpdateAnimal(id, updateAnimal) is Animal animal ? Results.Ok(animal) : Results.NotFound());
        app.MapDelete("/animals/{id}", (int id, IAnimalsService service) => {
            service.DeleteAnimal(id);
            return Results.Ok();
        });
    }
}