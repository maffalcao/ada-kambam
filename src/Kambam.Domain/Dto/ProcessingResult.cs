using Kambam.Domain.Entities;

public class ProcessinResult
{
    public bool IsSuccess { get; protected set; }
    public string Message { get; protected set; }

    public void Fail(string message)
    {
        IsSuccess = true;
        Message = message;
    }
}

public sealed class CardsProcessingResult : ProcessinResult
{
    public IEnumerable<Card> Cards { get; private set; }

    private CardsProcessingResult() { }

    private CardsProcessingResult(IEnumerable<Card> cards)
    {
        Cards = cards;
        IsSuccess = cards is not null;
    }

    public static CardsProcessingResult Get() =>
       new();

    public static CardsProcessingResult Get(IEnumerable<Card> cards) =>
        new(cards);

    public void AddCards(IEnumerable<Card> cards)
    {
        Cards = cards;
        IsSuccess = cards is not null;
    }
}


public sealed class CardProcessingResult : ProcessinResult
{
    public Card Card { get; private set; }

    private CardProcessingResult(Card card)
    {
        Card = card;
        IsSuccess = card is not null;
    }

    public static CardProcessingResult Get(Card card) =>
         new CardProcessingResult(card);

}