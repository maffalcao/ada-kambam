using System.Net;
using Kambam.Service.Dtos;
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
    public async Task<ActionResult<List<CardWithIdDto>>> GetAll()
    {
        var result = await _cardService.GetAll();

        if (result.IsSuccess is false)
            return NoContent();

        return Ok(result.Cards);
    }

    [HttpPost()]
    public async Task<ActionResult<CardWithIdDto>> Insert([FromBody] CardDto cardDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _cardService.Add(cardDto);
        return Ok(result.Card);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CardWithIdDto>> Update([FromRoute] int id, [FromBody] CardDto cardDto)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _cardService.Change(id, cardDto);

        if (result.IsSuccess is false)
            return NotFound(result.Message);

        return Ok(result.Card);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<List<CardWithIdDto>>> Delete([FromRoute] int id)
    {
        var result = await _cardService.Remove(id);

        if (result.IsSuccess is false)
            return NotFound(result.Message);

        return Ok(result.Cards);
    }

}
