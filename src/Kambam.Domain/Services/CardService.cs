using Kambam.Domain.Entities;
using Kambam.Domain.Interfaces;

namespace Kambam.Domain.Services;

public class CardService : ICardService
{
    private readonly ICardRepository _repository;

    public CardService(ICardRepository repository)
    {
        _repository = repository;
    }

    public async Task<CardProcessingResult> Add(Card card)
    {
        var newCard = await _repository.InsertAsync(card);
        var result = CardProcessingResult.Get(newCard);

        if (newCard is null)
            result.Fail("Error trying to add a new card");

        return CardProcessingResult.Get(newCard);
    }

    public async Task<CardProcessingResult> Change(Card card)
    {
        var result = CardProcessingResult.Get(card);

        var cardExist = await _repository.ExistsAsync(card.Id);

        if (cardExist is false)
        {
            result.Fail($"Card {card.Id} does not exist");
            return result;
        }

        var changedCard = await _repository.UpdateAsync(card);

        if (changedCard is null)
            result.Fail($"Error trying to update a new card {card.Id}");

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

        var card = await _repository.GetByIdAsync(id);

        if (card is null)
        {
            result.Fail($"Card {card.Id} does not exist");
            return result;
        }

        await _repository.DeleteAsync(id);

        var cards = await _repository.GetAllAsync();
        result.AddCards(cards);

        return result;


    }

}