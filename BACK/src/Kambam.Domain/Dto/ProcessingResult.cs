using System.Collections.ObjectModel;
using Kambam.Domain.Entities;

public class ProcessinResult
{
    public bool IsSuccess { get; protected set; }
    public string Message { get; protected set; }

    public void Fail(string message)
    {
        IsSuccess = false;
        Message = message;
    }
}

public sealed class CardsProcessingResult : ProcessinResult
{
    public IEnumerable<CardEntity> Cards { get; private set; }

    private CardsProcessingResult() { }

    private CardsProcessingResult(IEnumerable<CardEntity> cards)
    {
        Cards = cards;
        IsSuccess = cards is not null;
    }

    public static CardsProcessingResult Get() =>
       new();

    public static CardsProcessingResult Get(IEnumerable<CardEntity> cards) =>
        new(cards);

    public void AddCards(IEnumerable<CardEntity> cards)
    {
        Cards = cards;
        IsSuccess = cards is not null;
    }
}


public sealed class CardProcessingResult : ProcessinResult
{
    public CardEntity Card { get; private set; }

    private CardProcessingResult(CardEntity card)
    {
        Card = card;
        IsSuccess = card is not null;
    }

    public static CardProcessingResult Get(CardEntity card) =>
         new CardProcessingResult(card);

}