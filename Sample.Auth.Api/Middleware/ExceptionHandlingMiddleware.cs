using Newtonsoft.Json;
using Sample.Auth.Api.Exceptions;
using System.Net;
using System.Net.Mime;

namespace Sample.Auth.Api.Middleware
{
    public class ExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionHandlingMiddleware> _logger;

		public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			_logger.LogError(exception, "An unexpected error occurred");

			SampleException response = exception switch
			{
				ApplicationException _ => new SampleException(HttpStatusCode.BadRequest, "Bad request"),
				UnauthorizedAccessException _ => new SampleException(HttpStatusCode.Unauthorized, "Unauthorized"),
				_ => new SampleException(HttpStatusCode.InternalServerError, "Internal server error")
			};

			context.Response.ContentType = MediaTypeNames.Application.Json;
			context.Response.StatusCode = (int)response.StatusCode;
			await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
		}
	}
}
