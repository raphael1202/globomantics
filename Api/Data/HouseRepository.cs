using Microsoft.EntityFrameworkCore;

public interface IHouseRepository
{
    Task<IEnumerable<HouseDto>> GetAll();
    Task<HouseDetailDto?> Get(int id);
    Task<HouseDetailDto> Add(HouseDetailDto dto);
    Task<HouseDetailDto> Update(HouseDetailDto dto);
    Task Delete(int id);
}

public class HouseRepository : IHouseRepository
{
    private readonly HouseDbContext _context;
    public HouseRepository(HouseDbContext context)
    {
        _context = context;
    }

    private static void DtoEntity(HouseDetailDto dto, HouseEntity entity)
    {
        entity.Address = dto.Address;
        entity.Country = dto.Country;
        entity.Price = dto.Price;
        entity.Description = dto.Description;
        entity.Photo = dto.Photo;
    }

    private static HouseDetailDto EntityToDetailDto(HouseEntity entity)
    {
        return new HouseDetailDto(entity.Id, entity.Address, entity.Country, entity.Price, entity.Description, entity.Photo);
    }

    public async Task<IEnumerable<HouseDto>> GetAll()
    {
        return await _context.Houses.Select(h => new HouseDto(h.Id, h.Address, h.Description, h.Country, h.Price)).ToListAsync();
    }

    public async Task<HouseDetailDto?> Get(int id)
    {
        var e = await _context.Houses.SingleOrDefaultAsync(h => h.Id == id);

        if (e == null)
            return null;

        return EntityToDetailDto(e);
    }

    public async Task<HouseDetailDto> Add(HouseDetailDto dto)
    {
        var entity = new HouseEntity();
        DtoEntity(dto, entity);
        _context.Houses.Add(entity);

        await _context.SaveChangesAsync();

        return EntityToDetailDto(entity);
    }

    public async Task<HouseDetailDto> Update(HouseDetailDto dto)
    {
        var entity = await _context.Houses.FindAsync(dto.Id) ?? throw new ArgumentException($"Error updating house {dto.Id}");

        DtoEntity(dto, entity);

        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return EntityToDetailDto(entity);
    }

    public async Task Delete(int id)
    {
        var entity = await _context.Houses.FindAsync(id) ?? throw new ArgumentException($"Error deleting house {id}");

        _context.Houses.Remove(entity);
        await _context.SaveChangesAsync();
    }
}