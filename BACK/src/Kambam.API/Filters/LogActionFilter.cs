using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

public class LogActionFilter : IActionFilter
{
    private readonly ILogger<LogActionFilter> _logger;

    public LogActionFilter(ILogger<LogActionFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {

    }

    public void OnActionExecuted(ActionExecutedContext context)
    {

        if (context.HttpContext.Response.StatusCode == 200)
        {
            var actionName = context.ActionDescriptor.DisplayName;

            if (actionName.Contains("CardsController.Update") || actionName.Contains("CardsController.Delete"))
            {
                if (context.RouteData.Values.TryGetValue("id", out var id))
                {
                    var match = Regex.Match(actionName, @"\b(update|delete)\b", RegexOptions.IgnoreCase);
                    var utcNow = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss");
                    var message = $"{utcNow} - Card {id} - {match.Value}";

                    _logger.LogInformation(message);
                }
            }
        }
    }
}
