using Kambam.Domain.Entities;
using Kambam.Domain.Interfaces;
using Kambam.Service.Dtos;

namespace Kambam.Services.Services;

// TODO: use card dto's instead of card entities in returns & parameters
public class CardService : ICardService
{
    private readonly ICardRepository _repository;

    public CardService(ICardRepository repository)
    {
        _repository = repository;
    }

    public async Task<CardProcessingResult> Add(CardEntity card)
    {
        var newCard = await _repository.InsertAsync(card);
        var result = CardProcessingResult.Get(newCard);

        if (newCard is null)
            result.Fail("Error trying to add a new card");

        return CardProcessingResult.Get(newCard);
    }


    public async Task<CardProcessingResult> Change(CardEntity card)
    {
        var result = CardProcessingResult.Get(card);

        var changedCard = await _repository.UpdateAsync(card);

        if (changedCard is null)
        {
            result.Fail($"Card {card.Id} does not exist");
            return result;
        }

        return result;
    }

    public async Task<CardsProcessingResult> GetAll()
    {
        var cards = await _repository.GetAllAsync();
        return CardsProcessingResult.Get(cards);
    }

    public async Task<CardsProcessingResult> Remove(int id)
    {
        var result = CardsProcessingResult.Get();

        var deleteSuccessfully = await _repository.DeleteAsync(id);

        if (deleteSuccessfully is false)
        {
            result.Fail($"Card {id} does not exist");
            return result;
        }

        var cards = await _repository.GetAllAsync();
        result.AddCards(cards);

        return result;


    }

}