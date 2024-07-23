using Microsoft.EntityFrameworkCore;

public interface IBidRepository
{
    Task<List<BidDto>> Get(int houseId);
    Task<BidDto> Add(BidDto dto);
}

public class BidRepository : IBidRepository
{
    private readonly HouseDbContext _context;

    public BidRepository(HouseDbContext context)
    {
        _context = context;
    }

    public async Task<List<BidDto>> Get(int houseId)
    {
        return await _context.Bids.Where(b => b.HouseId == houseId)
            .Select(b => new BidDto(b.Id, b.HouseId, b.Bidder, b.Amount))
            .ToListAsync();
    }

    public async Task<BidDto> Add(BidDto dto)
    {
        var entity = new BidEntity
        {
            HouseId = dto.HouseId,
            Bidder = dto.Bidder,
            Amount = dto.Amount
        };

        _context.Bids.Add(entity);
        await _context.SaveChangesAsync();

        return new BidDto(entity.Id, entity.HouseId, entity.Bidder, entity.Amount);
    }
}