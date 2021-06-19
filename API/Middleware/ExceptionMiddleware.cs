using System;
using System.Net;
using API.Errors;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Infrastructure.Exceptions;

namespace API.Middleware
{
   public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddleware> _logger;
		private readonly IHostEnvironment _hostEnvironment;

		public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,
			IHostEnvironment hostEnvironment)
		{
			_next = next;
			_logger = logger;
			_hostEnvironment = hostEnvironment;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex) when (!(ex is CustomException))
			{
				_logger.LogError(ex, ex.Message);
				context.Response.ContentType = "application/json";
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

				var response = _hostEnvironment.IsDevelopment()
					? new ApiException((int)HttpStatusCode.InternalServerError,
						ex.Message,
						ex.StackTrace.ToString())
					: new ApiException((int)HttpStatusCode.InternalServerError);

				var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
				var json = JsonSerializer.Serialize(response, options);
				await context.Response.WriteAsync(json);
			}
		}
	}
}