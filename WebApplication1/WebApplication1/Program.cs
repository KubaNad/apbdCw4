using WebApplication1.Animals;
using WebApplication1.Visits;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAnimalsService, AnimalService>();
builder.Services.AddScoped<IVisitsService, VisitService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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
app.MapGet("/animals/{animalId}/visits", (int animalId, IVisitsService service) => 
    service.GetVisitsForAnimal(animalId));
app.MapPost("/visits", (Visit visit, IVisitsService service) => 
    Results.Created($"/visits/{visit.Id}", service.AddVisit(visit)));

app.UseHttpsRedirection();

app.Run();