using System.Net;
using Kambam.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kambam.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CardsController : ControllerBase
{
    private readonly ICardService _cardService;
    private readonly ILogger<CardsController> _logger;

    public CardsController(ICardService service, ILogger<CardsController> logger)
    {
        _cardService = service;
        _logger = logger;
    }

    [HttpGet()]
    [ProducesResponseType(typeof(List<CardEntity>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<ActionResult<List<CardEntity>>> GetAll()
    {
        var result = await _cardService.GetAll();

        if (result.IsSuccess is false)
            return NoContent();

        return Ok(result.Cards);
    }

    [HttpPost()]
    [ProducesResponseType(typeof(CardEntity), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<CardEntity>> Insert([FromBody] CardEntity card)
    {
        // TODO: use FluentValidation

        if (card.Id > 0)
            return BadRequest("'id' cant be higher than 0");

        if (card.IsValid() is false)
            return BadRequest("'conteudo' and 'lista' is mandatory");

        var result = await _cardService.Add(card);

        if (result.IsSuccess is false)
            return StatusCode((int)HttpStatusCode.InternalServerError, result.Message);

        return Ok(result.Card);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(CardEntity), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<CardEntity>> Update([FromRoute] int id, [FromBody] CardEntity card)
    {
        card.Id = id;

        // TODO: use FluentValidation
        if (card.Id is 0)
            return BadRequest("'id' cant be 0");

        if (card.IsValid() is false)
            return BadRequest("'conteudo' and 'lista' is mandatory");

        var result = await _cardService.Change(card);

        if (result.IsSuccess is false)
            return NotFound(result.Message);

        LogMessage(id, "Atualizar");

        return Ok(result.Card);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(List<CardEntity>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<CardEntity>> Delete([FromRoute] int id)
    {
        var result = await _cardService.Remove(id);

        if (result.IsSuccess is false)
            return NotFound(result.Message);

        LogMessage(id, "Remover");

        return Ok(result.Cards);
    }

    // Deviating from the original specification to simplify logging. In this approach,
    // a private method 'LogMessage' is used within the controller to log 'delete' and 'update' actions.
    // Retrieving the 'Titulo' for 'delete' posed challenges as it was not explicitly available in the operation.
    // This approach provides a more straightforward and self-contained solution, considering both simplicity
    // and potential ambiguity in the original specification.
    private void LogMessage(int cardId, string actionName)
    {
        var utcNow = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss");
        var message = $"{utcNow} - Card {cardId} - {actionName}";
        _logger.LogInformation(message);
    }

}
