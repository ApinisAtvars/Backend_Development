namespace Sneakers.API.Middleware;

public class ApiKeyMiddleware
{
    // HTTP request flows through a pipeline
    // We are now putting a new point between the endpoint and the other blocks
    private readonly RequestDelegate _next;

    // The name of the header that will contain the API key
    private const string APIKEY = "XApiKey";
    // The next block that needs to be executed sits here
    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        // First, find out whether an API key is provided
        // Extract it, if it is found
        // If it's not found, immediately throw a 401 error, so the request doesn't even get to the API
        if (!context.Request.Headers.TryGetValue(APIKEY, out
                var extractedApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Api Key was not provided ");
            return;
        }
        // Return the object from the service container
        var appSettings = context.RequestServices.GetRequiredService<IOptions<APIKeySettings>>();
        var apiKey = appSettings.Value.ApiKey;
        // If it's the wrong API key, throw a 401 error
        if (!apiKey.Equals(extractedApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized client");
            return;
        }
        // If the API key is correct, continue with the request
        await _next(context);
    }
}

