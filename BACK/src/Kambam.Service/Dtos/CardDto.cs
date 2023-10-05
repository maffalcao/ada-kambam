using System.ComponentModel.DataAnnotations;

namespace Kambam.Service.Dtos;

public class CardDto
{
    public string Titulo { get; set; }
    [Required(ErrorMessage = "Conteudo is mandatory")]
    public string Conteudo { get; set; }
    [Required(ErrorMessage = "Lista is mandatory")]
    public string Lista { get; set; }

    public CardDto(string titulo, string conteudo, string lista)
    {
        Titulo = titulo;
        Conteudo = conteudo;
        Lista = lista;
    }

    public CardDto() { }
}

public class CardWithIdDto : CardDto
{
    public int Id { get; set; }

    public CardWithIdDto(int id, string titulo, string conteudo, string lista) : base(titulo, conteudo, lista)
    {
        Id = id;
    }

    public CardWithIdDto() { }
}
