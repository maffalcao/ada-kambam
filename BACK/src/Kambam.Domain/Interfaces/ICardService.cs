using Kambam.Domain.Entities;

public interface ICardService
{
    Task<CardsProcessingResult> GetAll();
    Task<CardProcessingResult> Add(Card card);
    Task<CardProcessingResult> Change(Card card);
    Task<CardsProcessingResult> Remove(int id);

}