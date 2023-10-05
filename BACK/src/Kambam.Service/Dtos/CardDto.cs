namespace Kambam.Service.Dtos;

public class CardDto
{
    public string Titulo { get; set; }
    public string Conteudo { get; set; }
    public string Lista { get; set; }
}

public class CardWithIdDto : CardDto
{
    public int Id { get; set; }
}
