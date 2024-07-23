using Microsoft.AspNetCore.Mvc;
using MiniValidation;

public static class WebApplicationBidExtension
{
    public static void MapBidEndpoints(this WebApplication app)
    {
        app.MapGet("house/{houseId:int}/bids", async (int houseId, IHouseRepository houseRepo, IBidRepository bidRepo) =>
        {
            if (await houseRepo.Get(houseId) == null)
                Results.Problem($"House with id {houseId} not found", statusCode: StatusCodes.Status404NotFound);

            var bids = await bidRepo.Get(houseId);
            return Results.Ok(bids);
        }).ProducesProblem(404).Produces<List<BidDto>>(StatusCodes.Status200OK);

        app.MapPost("house/{houseId:int}/bids", async (int houseId, [FromBody] BidDto dto, IHouseRepository houseRepo, IBidRepository bidRepo) =>
        {
            if (await houseRepo.Get(houseId) == null)
                Results.Problem($"House with id {houseId} not found", statusCode: StatusCodes.Status404NotFound);

            if (dto.HouseId != houseId)
                Results.Problem("HouseId in the URL does not match the HouseId in the body", statusCode: StatusCodes.Status400BadRequest);

            if (!MiniValidator.TryValidate(dto, out var errors))
                return Results.ValidationProblem(errors);

            var newBid = await bidRepo.Add(dto);

            return Results.Created($"/house/{newBid.HouseId}/bids", newBid);
        }).ProducesProblem(404).ProducesProblem(400).ProducesValidationProblem().Produces<BidDto>(StatusCodes.Status201Created);
    }
}