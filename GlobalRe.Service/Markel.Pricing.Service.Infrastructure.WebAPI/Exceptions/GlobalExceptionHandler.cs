using Markel.Pricing.Service.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace Markel.Pricing.Service.Infrastructure.Exceptions
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        private const string OUT_OF_MEMORY_EXCEPTION_MESSAGE = "Out of Memory";
        private const string TIMEOUT_EXCEPTION_MESSAGE = "The wait operation timed out";

        public override void Handle(ExceptionHandlerContext context)
        {
            HttpResponseMessage response = null;

            //Check the Exception Type
            if (context.Exception is BadRequestException) response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            else if (context.Exception is NotFoundException) response = new HttpResponseMessage(HttpStatusCode.NotFound);

            //The Response Message Set by the Action During Ececution
            if (response != null)
            {
                var res = context.Exception.Message;
                response.Content = new StringContent(res);
                response.ReasonPhrase = res;

                //Create the Error Response
                context.ExceptionContext.Response = response;
            }

            base.Handle(context);
        }

        public async override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            HandleException(context);
            await base.HandleAsync(context, cancellationToken);
        }

        private void HandleException(ExceptionHandlerContext context)
        {
            string errorDescription, errorMessage;
            List<APIMessage> details = new List<APIMessage>();
            HttpStatusCode responseCode = HttpStatusCode.InternalServerError;

            APIException apiException = GetAPIException(context.Exception);
            if (apiException != null)
            {
                responseCode = (HttpStatusCode)apiException.HttpStatusCode;
                errorDescription = apiException.GetType().Name;
                errorMessage = apiException.Message;

                APIMessageException apiMessageException = apiException as APIMessageException;
                if (apiMessageException != null)
                {
                    details.AddRange(apiMessageException.Details);
                }
            }
            else
            {
                errorDescription = "UnhandledException";
                errorMessage = "An unexpected error occurred.";
            }

            HttpResponseMessage response = context.Request.CreateResponse(responseCode, new
            {
                Message = errorMessage,
                Details = details
            });
            response.Headers.Add("Error", errorDescription);
            response.ApplyRequestIDHeader(context.Request);
            context.Result = new ResponseMessageResult(response);
        }

        private static APIException GetAPIException(Exception ex)
        {
            while (ex != null)
            {
                if (ex is APIException)
                {
                    return ex as APIException;
                }
                else if (ex is OutOfMemoryException)
                {
                    return new InsufficientStorageAPIException(OUT_OF_MEMORY_EXCEPTION_MESSAGE);
                }
                else if (ex is Win32Exception && ex.Message.Equals(TIMEOUT_EXCEPTION_MESSAGE))
                {
                    return new ServiceUnavailableAPIException(TIMEOUT_EXCEPTION_MESSAGE);
                }

                ex = ex.InnerException;
            }

            return null;
        }

        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            return true;
        }

        public class ErrorMessageResult : IHttpActionResult
        {
            private HttpRequestMessage _request;
            private HttpResponseMessage _httpResponseMessage;

            public ErrorMessageResult(HttpRequestMessage request, HttpResponseMessage httpResponseMessage)
            {
                _request = request;
                _httpResponseMessage = httpResponseMessage;
            }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                return Task.FromResult(_httpResponseMessage);
            }
        }
    }
}
