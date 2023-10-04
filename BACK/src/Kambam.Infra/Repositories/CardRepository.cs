using System.Collections.ObjectModel;
using Kambam.Domain.Entities;
using Kambam.Domain.Interfaces;
using Kambam.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Kambam.Infra.Repositories;

public class CardRepository : ICardRepository
{
    private readonly MyContext _context;
    protected DbSet<CardEntity> _dataSet;
    public CardRepository(MyContext context)
    {
        _context = context;
        _dataSet = context.Set<CardEntity>();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);

        if (entity == null)
            return false;

        _dataSet.Remove(entity);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        var entity = await GetByIdAsync(id);

        return entity is not null;
    }

    public async Task<IEnumerable<CardEntity>> GetAllAsync()
    {
        return await _dataSet.ToListAsync();
    }

    public async Task<CardEntity> GetByIdAsync(int id)
    {
        return await _dataSet.SingleOrDefaultAsync(_ => _.Id.Equals(id));
    }

    public async Task<CardEntity> InsertAsync(CardEntity card)
    {
        var entity = await GetByIdAsync(card.Id);

        if (entity == null)
            return null;

        _dataSet.Remove(entity);
        await _context.SaveChangesAsync();

        return card;
    }

    public async Task<CardEntity> UpdateAsync(CardEntity card)
    {
        var dbEntity = await GetByIdAsync(card.Id);

        if (dbEntity == null)
            return null;

        _context.Entry(dbEntity).CurrentValues.SetValues(card);
        await _context.SaveChangesAsync();

        return card;
    }

}