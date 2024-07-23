using Microsoft.AspNetCore.Mvc;
using MiniValidation;

public static class WebApplicationHouseExtension
{
    public static void MapHouseEndpoints(this WebApplication app)
    {

        app.MapGet("/houses", (IHouseRepository repo) => repo.GetAll()).Produces<HouseDetailDto>(StatusCodes.Status200OK);

        app.MapGet("/house/{houseId:int}", async (int houseId, IHouseRepository repo) =>
        {
            var house = await repo.Get(houseId);

            if (house == null)
                return Results.Problem($"House with id {houseId} not found", statusCode: StatusCodes.Status404NotFound);

            return Results.Ok(house);
        }).ProducesProblem(404).Produces<HouseDetailDto>(StatusCodes.Status200OK);

        app.MapPost("/houses", async ([FromBody] HouseDetailDto house, IHouseRepository repo) =>
        {
            if (!MiniValidator.TryValidate(house, out var errors))
                return Results.ValidationProblem(errors);

            var newHouse = await repo.Add(house);

            return Results.Created($"/house/{newHouse.Id}", newHouse);
        }).Produces<HouseDetailDto>(StatusCodes.Status201Created).ProducesValidationProblem();

        app.MapPut("/houses", async ([FromBody] HouseDetailDto house, IHouseRepository repo) =>
        {
            if (!MiniValidator.TryValidate(house, out var errors))
                return Results.ValidationProblem(errors);

            if (await repo.Get(house.Id) == null)
                return Results.Problem($"House with id {house.Id} not found", statusCode: StatusCodes.Status404NotFound);

            var updatedHouse = repo.Update(house);

            return Results.Ok(updatedHouse);
        }).ProducesProblem(404).Produces<HouseDetailDto>(StatusCodes.Status200OK).ProducesValidationProblem();

        app.MapDelete("/houses/{houseId:int}", async (int houseId, IHouseRepository repo) =>
        {
            if (await repo.Get(houseId) == null)
                return Results.Problem($"House with Id {houseId} not found", statusCode: 404);

            await repo.Delete(houseId);

            return Results.Ok();
        }).ProducesProblem(404).Produces(StatusCodes.Status200OK);
    }
}