using Kambam.Domain.Entities;
using Kambam.Service.Dtos;

public interface ICardService
{
    Task<CardsProcessingResult> GetAll();
    Task<CardProcessingResult> Add(CardEntity card);
    Task<CardProcessingResult> Change(CardEntity card);
    Task<CardsProcessingResult> Remove(int id);
}