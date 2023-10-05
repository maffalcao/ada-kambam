using Moq;
using AutoMapper;
using FluentAssertions;
using Kambam.API.Mapper;
using Kambam.Service.Dtos;
using Kambam.Domain.Entities;
using Kambam.Domain.Interfaces;
using Kambam.Services.Services;
using Bogus;

namespace Kambam.Tests.Service;
public class CardServiceTests
{
    private readonly IMapper _mapper;
    private readonly Faker _faker;

    public CardServiceTests()
    {
        _faker = new Faker();
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CardMapperProfile>();
        });
        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task Change_ValidCardId_ReturnsSuccessResult()
    {
        // Arrange        
        var cardId = _faker.Random.Int(1, 100);
        var cardTitulo = _faker.Random.String2(50);
        var cardConteudo = _faker.Random.String2(200);
        var cardLista = _faker.Random.String2(10);

        var cardDto = new CardDto(cardTitulo, cardConteudo, cardLista);

        var cardEntity = new CardEntity(cardTitulo, cardConteudo, cardLista);
        cardEntity.SetId(cardId);

        var repositoryMock = new Mock<ICardRepository>();
        repositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<CardEntity>()))
            .ReturnsAsync(cardEntity);

        var service = new CardService(repositoryMock.Object, _mapper);

        // Act
        var result = await service.Change(cardId, cardDto);

        // Assert
        result.Should().NotBeNull()
              .And.Match<CardServiceResult>(r => r.IsSuccess)
              .And.Match<CardServiceResult>(r => r.Card != null);
        result.Message.Should().BeNull();

        result.Card.Id.Should().Be(cardId);
        result.Card.Titulo.Should().Be(cardDto.Titulo);
        result.Card.Conteudo.Should().Be(cardDto.Conteudo);
        result.Card.Lista.Should().Be(cardDto.Lista);

    }

    [Fact]
    public async Task Change_InvalidCardId_ReturnsFailureResult()
    {
        // Arrange
        var cardId = _faker.Random.Int(1, 100);
        var cardTitulo = _faker.Random.String2(50);
        var cardConteudo = _faker.Random.String2(200);
        var cardLista = _faker.Random.String2(10);

        var cardDto = new CardDto(cardTitulo, cardConteudo, cardLista);

        var repositoryMock = new Mock<ICardRepository>();
        repositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<CardEntity>()))
            .ReturnsAsync((CardEntity)null);

        var service = new CardService(repositoryMock.Object, _mapper);

        // Act
        var result = await service.Change(cardId, cardDto);

        // Assert
        result.Should().NotBeNull()
              .And.Match<CardServiceResult>(r => !r.IsSuccess);
        result.Message.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Remove_ExistingCardId_ReturnsSuccessResult()
    {
        // Arrange
        var cardId = _faker.Random.Int(1, 100);

        var repositoryMock = new Mock<ICardRepository>();
        repositoryMock.Setup(repo => repo.DeleteAsync(cardId))
            .ReturnsAsync(true);

        var service = new CardService(repositoryMock.Object, _mapper);

        // Act
        var result = await service.Remove(cardId);

        // Assert
        result.Should().NotBeNull()
              .And.Match<CardsServiceResult>(r => r.IsSuccess);
        result.Message.Should().BeNull();

    }

    [Fact]
    public async Task Remove_NonExistingCardId_ReturnsFailureResult()
    {
        // Arrange
        var cardId = _faker.Random.Int(1, 100);

        var repositoryMock = new Mock<ICardRepository>();
        repositoryMock.Setup(repo => repo.DeleteAsync(cardId))
            .ReturnsAsync(false);

        var service = new CardService(repositoryMock.Object, _mapper);

        // Act
        var result = await service.Remove(cardId);

        // Assert
        result.Should().NotBeNull()
              .And.Match<CardsServiceResult>(r => !r.IsSuccess);
        result.Message.Should().NotBeNullOrEmpty();
    }
}
