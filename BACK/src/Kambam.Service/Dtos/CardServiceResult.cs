using System.Collections.ObjectModel;
using Kambam.Domain.Entities;

namespace Kambam.Service.Dtos;

public class ServiceResult
{
    public bool IsSuccess { get; protected set; }
    public string Message { get; protected set; }
}

public sealed class CardsServiceResult : ServiceResult
{
    public IEnumerable<CardWithIdDto> Cards { get; private set; }

    private CardsServiceResult() { }

    private CardsServiceResult(IEnumerable<CardWithIdDto> dtos)
    {
        Cards = dtos;
        IsSuccess = dtos is not null;
    }

    public static CardsServiceResult Get() =>
       new();

    public static CardsServiceResult Get(IEnumerable<CardWithIdDto> dtos) =>
        new(dtos);

    public void AddCards(IEnumerable<CardWithIdDto> dtos)
    {
        Cards = dtos;
        IsSuccess = dtos is not null;
    }

    public CardsServiceResult Fail(string message)
    {
        IsSuccess = false;
        Message = message;

        return this;
    }

}


public sealed class CardServiceResult : ServiceResult
{
    public CardWithIdDto Card { get; private set; }

    private CardServiceResult(CardWithIdDto dto)
    {
        Card = dto;
        IsSuccess = dto is not null;
    }

    private CardServiceResult() { }

    public static CardServiceResult Get(CardWithIdDto dto) =>
         new CardServiceResult(dto);

    public static CardServiceResult Get() =>
         new CardServiceResult();

    public CardServiceResult Fail(string message)
    {
        IsSuccess = false;
        Message = message;

        return this;
    }

    public CardServiceResult AddCard(CardWithIdDto dto)
    {
        Card = dto;
        IsSuccess = dto is not null;

        return this;
    }

}