namespace Kambam.Domain.Entities;

public class CardEntity
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Conteudo { get; set; }
    public string Lista { get; set; }

    public bool IsValid()
    {
        return (Conteudo is not null) && (Lista is not null);
    }

    public void SetId(int id)
    {
        Id = id;
    }
}