using Kambam.Domain.Dto;

public interface ICardService
{
    IEnumerable<CardDto> GetAll();
    CardDto Add(CardDto cardDto);
    CardDto Change(CardDto cardDto);
    IEnumerable<CardDto> Remove(int id);

}