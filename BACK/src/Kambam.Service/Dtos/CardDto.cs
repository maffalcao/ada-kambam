using System.ComponentModel.DataAnnotations;

namespace Kambam.Service.Dtos;

public class CardDto
{
    public string Titulo { get; set; }
    [Required(ErrorMessage = "Conteudo is mandatory")]
    public string Conteudo { get; set; }
    [Required(ErrorMessage = "Lista is mandatory")]
    public string Lista { get; set; }
}

public class CardWithIdDto : CardDto
{
    public int Id { get; set; }
}
