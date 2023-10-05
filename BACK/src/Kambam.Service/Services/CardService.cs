using AutoMapper;
using Kambam.Domain.Entities;
using Kambam.Domain.Interfaces;
using Kambam.Service.Dtos;

namespace Kambam.Services.Services;

// We'll use a mapper to shield the API layer from direct access to domain entities.
public class CardService : ICardService
{
    private readonly ICardRepository _repository;
    private readonly IMapper _mapper;

    public CardService(ICardRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CardServiceResult> Add(CardDto cardDto)
    {
        var result = CardServiceResult.Get();

        var cardToAdd = _mapper.Map<CardEntity>(cardDto);
        var newCard = await _repository.InsertAsync(cardToAdd);
        var cardWithIdDto = _mapper.Map<CardWithIdDto>(newCard);

        return result.AddCard(cardWithIdDto);
    }


    public async Task<CardServiceResult> Change(int cardId, CardDto cardDto)
    {
        var result = CardServiceResult.Get();

        var cardToChange = _mapper.Map<CardEntity>(cardDto);
        cardToChange.SetId(cardId);

        var changedCard = await _repository.UpdateAsync(cardToChange);

        if (changedCard is null)
        {
            return result.Fail($"Card {cardToChange.Id} does not exist");
        }

        var cardWithIdDto = _mapper.Map<CardWithIdDto>(changedCard);

        return result.AddCard(cardWithIdDto);
    }

    public async Task<CardsServiceResult> GetAll()
    {
        var cards = await _repository.GetAllAsync();
        var cardDtos = _mapper.Map<List<CardWithIdDto>>(cards);

        return CardsServiceResult.Get().AddCards(cardDtos);
    }

    public async Task<CardsServiceResult> Remove(int id)
    {
        var result = CardsServiceResult.Get();

        var deleteSuccessfully = await _repository.DeleteAsync(id);

        if (deleteSuccessfully is false)
        {
            return result.Fail($"Card {id} does not exist");
        }

        return await GetAll();
    }
}