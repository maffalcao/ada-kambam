using System.ComponentModel.DataAnnotations;
using Bogus;
using Kambam.Service.Dtos;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace Kambam.Tests.API;

public class DtoValidationTests
{
    [Fact]
    public void CardDto_Validation_WithEmptyConteudo_ShouldFail()
    {
        // Arrange
        var cardDto = new CardDto
        {
            Titulo = "Sample Title",
            Conteudo = string.Empty, // Conteudo está vazio
            Lista = "Sample List"
        };

        var context = new ValidationContext(cardDto, null, null);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(cardDto, context, results, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(results, r => r.MemberNames.Contains("Conteudo"));
    }

    [Fact]
    public void CardDto_Validation_WithNullConteudo_ShouldFail()
    {
        // Arrange
        var cardDto = new CardDto
        {
            Titulo = "Sample Title",
            Conteudo = null, // Conteudo é nulo
            Lista = "Sample List"
        };

        var context = new ValidationContext(cardDto, null, null);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(cardDto, context, results, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(results, r => r.MemberNames.Contains("Conteudo"));
    }

    [Fact]
    public void CardDto_Validation_WithEmptyLista_ShouldFail()
    {
        // Arrange
        var cardDto = new CardDto
        {
            Titulo = "Sample Title",
            Conteudo = "Sample Content",
            Lista = string.Empty, // Lista está vazia
        };

        var context = new ValidationContext(cardDto, null, null);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(cardDto, context, results, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(results, r => r.MemberNames.Contains("Lista"));
    }

    [Fact]
    public void CardDto_Validation_WithNullLista_ShouldFail()
    {
        // Arrange
        var cardDto = new CardDto
        {
            Titulo = "Sample Title",
            Conteudo = "Sample Content",
            Lista = null, // Lista é nula
        };

        var context = new ValidationContext(cardDto, null, null);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(cardDto, context, results, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(results, r => r.MemberNames.Contains("Lista"));
    }

    [Fact]
    public void CardDto_Validation_WithAllRequiredProperties_ShouldPass()
    {
        // Arrange
        var cardDto = new CardDto
        {
            Titulo = "Sample Title",
            Conteudo = "Sample Content",
            Lista = "Sample List"
        };

        var context = new ValidationContext(cardDto, null, null);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(cardDto, context, results, true);

        // Assert
        Assert.True(isValid);
        Assert.Empty(results);
    }
}