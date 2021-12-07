using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoServiceAPI {
    public class APIMiddleware {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public APIMiddleware(RequestDelegate next, ILoggerFactory loggerfactory) {
            _next = next;
            _logger = loggerfactory.CreateLogger<APIMiddleware>();
        }

        public async Task Invoke(HttpContext context) {
            try {
                await _next(context);
            }
            finally {
                _logger.LogInformation(
                    "{CurrentTime} - Request {Method} {url} => {statusCode}",
                    DateTime.Now,
                    context.Request?.Method,
                    context.Request?.Path.Value,
                    context.Response?.StatusCode);
            }
        }
    }
}
