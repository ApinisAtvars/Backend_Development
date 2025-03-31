namespace Sneakers.API.Middleware;

public class ApiKeyMiddlewareDb
{
    // HTTP request flows through a pipeline
    // We are now putting a new point between the endpoint and the other blocks
    private readonly RequestDelegate _next;

    // The name of the header that will contain the API key
    private const string APIKEY = "XApiKey";
    // The next block that needs to be executed sits here
    public ApiKeyMiddlewareDb(RequestDelegate next)
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
        var mongoService = context.RequestServices.GetRequiredService<IMongoService>();
        var user = await mongoService.GetUserByApiKey(extractedApiKey);

        if (user==null)
        {
            await context.Response.WriteAsync("Unauthorized client");
            return;
        }

        context.Items["CUSTOMERNR"] = user.CustomerNr; // Store customer number inside http context, we will need this later to calculate the discount
        
        await _next(context);
    }
}

