using Kambam.API.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kambam.API.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{

    private readonly IJWTManagerRepository _jWTManager;

    public LoginController(IJWTManagerRepository jWTManager)
    {
        _jWTManager = jWTManager;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("")]
    public IActionResult Authenticate(Users user)
    {
        var token = _jWTManager.Authenticate(user);

        if (token == null)
        {
            return Unauthorized();
        }

        return Ok(token);
    }



}