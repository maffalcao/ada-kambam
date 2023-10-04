using System.Collections.ObjectModel;
using Kambam.Domain.Entities;

namespace Kambam.Domain.Interfaces;

public interface ICardRepository
{
    Task<bool> ExistsAsync(int id);
    Task<CardEntity> GetByIdAsync(int id);
    Task<IEnumerable<CardEntity>> GetAllAsync();
    Task<CardEntity> InsertAsync(CardEntity card);
    Task<CardEntity> UpdateAsync(CardEntity card);
    Task<bool> DeleteAsync(int id);
}