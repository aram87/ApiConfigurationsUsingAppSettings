using ApiConfigurationsUsingAppSettings.Interfaces;
using ApiConfigurationsUsingAppSettings.Models;
using Microsoft.Extensions.Options;

namespace ApiConfigurationsUsingAppSettings.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHashingService _hashingService;
        private readonly IOptions<ApiSettingsModel> _options;
        private const string APIKEYNAME = "ApiKey";
        public ApiKeyMiddleware(RequestDelegate next, IHashingService hashingService, IOptions<ApiSettingsModel> options)
        {
            _next = next;
            _hashingService = hashingService;
            _options = options;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api Key was not provided. (Using ApiKeyMiddleware) ");
                return;
            }

            byte[] apiKey = System.Text.Encoding.ASCII.GetBytes(extractedApiKey!);

            var extractedApiKeyHashed = _hashingService.HashUsingPbkdf2(apiKey);

            var apiKeyHash = _options.Value.ApiKeyHash ?? "";
            if (!apiKeyHash.Equals(extractedApiKeyHashed))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized client. (Using ApiKeyMiddleware)");
                return;
            }
            await _next(context);
        }
    }
}