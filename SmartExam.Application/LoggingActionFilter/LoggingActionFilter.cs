using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.LoggingActionFilter
{
    public class LoggingActionFilter: IAsyncActionFilter
    {
        private readonly ILogger<LoggingActionFilter> _logger;

        public LoggingActionFilter(ILogger<LoggingActionFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var dateTimeNow = DateTime.UtcNow; // Or use DateTime.Now for local time

            // Before the action executes
            _logger.LogInformation("{DateTime} - Executing action {ActionName} on controller {ControllerName} with parameters {ActionArguments}",
                dateTimeNow,
                context.ActionDescriptor.DisplayName,
                context.Controller.ToString(),
                context.ActionArguments);

            // Execute the action
            var resultContext = await next();

            // After the action executes
            if (resultContext.Exception == null)
            {
                _logger.LogInformation("{DateTime} - Executed action {ActionName} on controller {ControllerName} successfully",
                    dateTimeNow,
                    context.ActionDescriptor.DisplayName,
                    context.Controller.ToString());
            }
            else
            {
                _logger.LogError(resultContext.Exception, "{DateTime} - Action {ActionName} on controller {ControllerName} threw an exception",
                    dateTimeNow,
                    context.ActionDescriptor.DisplayName,
                    context.Controller.ToString());
            }
        }

    }
}
