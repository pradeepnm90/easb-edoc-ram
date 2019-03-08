using Markel.Pricing.Service.Infrastructure.Config;
using Markel.Pricing.Service.Infrastructure.Logging;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace Markel.Pricing.Service.Infrastructure.Exceptions
{
    public class GlobalExceptionLogger : ExceptionLogger
    {
        private static string ApiExceptionLogFilter = MarkelConfiguration.ApiExceptionLogFilter;

        public GlobalExceptionLogger() { }

        public override Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            // Only log unhandled exceptions (non custom API Exceptions)
            if (IsLoggable(context?.Exception))
            {
                ILogManager logManager = context.Request.GetDependencyScope().GetService(typeof(ILogManager)) as ILogManager;
                LogModel logModel = context.Request.GetLogModel(context.Exception.Message, MarkelConfiguration.ApplicationName);
                logManager.LogMessage(logModel, context.Exception);
            }

            return base.LogAsync(context, cancellationToken);
        }

        private static bool IsLoggable(Exception exception)
        {
            if (exception == null) return false;

            // API Exception
            if (exception is APIException)
            {
                if (string.IsNullOrEmpty(ApiExceptionLogFilter)) return false;

                Regex exceptionLogFilterRegex = new Regex(ApiExceptionLogFilter);
                return exceptionLogFilterRegex.IsMatch(exception.GetType().Name);
            }

            return true;
        }
    }
}
