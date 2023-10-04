using Kambam.Domain.Entities;

namespace Kambam.Domain.Interfaces;

public interface ICardRepository
{
    IEnumerable<Card> GetAll();
    Card Insert(Card card);
    Card Update(Card card);
    IEnumerable<Card> Delete(int cardId);

}