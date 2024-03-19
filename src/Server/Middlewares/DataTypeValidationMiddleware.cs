using System.Text.Json;
using WirsolutViajes.Shared.Wrapper;

namespace WirsolutViajes.Server.Middlewares
{
    public class DataTypeValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public DataTypeValidationMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            foreach (var queryParameter in context.Request.Query)
            {
                if (!IsOfType(queryParameter.Value))
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    var errorMessage = $"El valor '{queryParameter.Value}' para el parámetro '{queryParameter.Key}' no es de un tipo de datos válido.";
                    var responseModel = await Result<string>.FailAsync(errorMessage);
                    var result = JsonSerializer.Serialize(responseModel);
                    await context.Response.WriteAsync(result);

                    _logger.LogError(errorMessage);

                    return;


                }
            }

            await _next(context);
        }

        private bool IsOfType(string value)
        {
            return int.TryParse(value, out _) ||
                   long.TryParse(value, out _) ||
                   float.TryParse(value, out _) ||
                   double.TryParse(value, out _) ||
                   decimal.TryParse(value, out _) ||
                   DateTime.TryParse(value, out _);
        }
    }
}
