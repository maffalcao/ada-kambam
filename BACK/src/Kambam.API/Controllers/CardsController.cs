using System.Net;
using Kambam.API.Extensions;
using Kambam.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Kambam.API.Controllers;

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
    [ProducesResponseType(typeof(List<Card>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<ActionResult<List<Card>>> GetCards()
    {
        var result = await _cardService.GetAll();

        if (result.IsSuccess is false)
            return NoContent();

        return Ok(result.Cards);
    }

    [HttpPost()]
    [ProducesResponseType(typeof(Card), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<Card>> InsertCard([FromBody] Card card)
    {
        // TODO: use FluentValidation
        if (card.Id > 0)
            return BadRequest("'id' cant be higher than 0");

        if (card.Conteudo.IsNullOrEmpty())
            return BadRequest("'conteudo' is mandatory");

        if (card.Lista.IsNullOrEmpty())
            return BadRequest("'lista' is mandatory");

        var result = await _cardService.Add(card);

        if (result.IsSuccess is false)
            return StatusCode((int)HttpStatusCode.InternalServerError, result.Message);

        return Ok(result.Card);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(Card), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<Card>> UpdateCard([FromRoute] int id, [FromBody] Card card)
    {
        card.Id = id;

        // TODO: use FluentValidation
        if (card.Id is 0)
            return BadRequest("'id' cant be 0");

        if (card.Conteudo.IsNullOrEmpty())
            return BadRequest("'conteudo' is mandatory");

        if (card.Lista.IsNullOrEmpty())
            return BadRequest("'lista' is mandatory");

        var result = await _cardService.Change(card);

        if (result.IsSuccess is false)
            return BadRequest(result.Message);

        return Ok(result.Card);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(List<Card>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<Card>> DeleteCard([FromRoute] int id)
    {
        var result = await _cardService.Remove(id);

        if (result.IsSuccess is false)
            return BadRequest(result.Message);

        return Ok(result.Cards);
    }








}
