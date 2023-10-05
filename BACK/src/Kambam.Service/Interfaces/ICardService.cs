using Kambam.Domain.Entities;
using Kambam.Service.Dtos;

public interface ICardService
{
    Task<CardsServiceResult> GetAll();
    Task<CardServiceResult> Add(CardDto cardDto);
    Task<CardServiceResult> Change(int cardId, CardDto cardWithIdDto);
    Task<CardsServiceResult> Remove(int id);
}