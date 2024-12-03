using System.Security.Claims;

public class UserActivityMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<UserActivityMiddleware> _logger;

    public UserActivityMiddleware(RequestDelegate next, ILogger<UserActivityMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity.IsAuthenticated)
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var sessionId = context.User.FindFirst("SessionId")?.Value;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(sessionId))
            {
                _logger.LogInformation("User is authenticated. UserId: {UserId}, SessionId: {SessionId}", userId, sessionId);

                var userLogsService = context.RequestServices.GetRequiredService<IUserLogsService>();
                var pageVisited = context.Request.Path.Value;

                // Filter out unwanted routes
                if (!pageVisited.StartsWith("/_") && pageVisited != "/favicon.ico")
                {
                    await userLogsService.LogActivityAsync(new UserLogsModel
                    {
                        UserId = userId,
                        PagesVisited = pageVisited // Log the full path
                    });

                    // _logger.LogInformation("Page visited logged: {PageVisited}", pageVisited);
                }
            }
            else
            {
                _logger.LogWarning("UserId or SessionId is null or empty. UserId: {UserId}, SessionId: {SessionId}", userId, sessionId);
            }
        }
        else
        {
            _logger.LogWarning("User is not authenticated.");
        }

        await _next(context);
    }

}