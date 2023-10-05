namespace Kambam.Domain.Entities;

public class CardEntity
{
    public int Id { get; private set; }
    public string Titulo { get; private set; }
    public string Conteudo { get; private set; }
    public string Lista { get; private set; }

    public bool IsValid()
    {
        return (Conteudo is not null) && (Lista is not null);
    }

    public void SetId(int id)
    {
        Id = id;
    }

    public CardEntity(string titulo, string conteudo, string lista)
    {
        Titulo = titulo;
        Conteudo = conteudo;
        Lista = lista;
    }
}