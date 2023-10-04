using Kambam.Domain.Entities;

namespace Kambam.Domain.Interfaces;

public interface ICardRepository
{
    Task<bool> ExistsAsync(int id);
    Task<Card> GetByIdAsync(int id);
    Task<IEnumerable<Card>> GetAllAsync();
    Task<Card> InsertAsync(Card card);
    Task<Card> UpdateAsync(Card card);
    Task<IEnumerable<Card>> DeleteAsync(int cardId);
}