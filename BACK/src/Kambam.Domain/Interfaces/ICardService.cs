using Kambam.Domain.Entities;

public interface ICardService
{
    Task<CardsProcessingResult> GetAll();
    Task<CardProcessingResult> Add(CardEntity card);
    Task<CardProcessingResult> Change(CardEntity card);
    Task<CardsProcessingResult> Remove(int id);

}