using Markel.Pricing.Service.Infrastructure.Dependency;
using Markel.Pricing.Service.Infrastructure.Logging.Entities;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;

namespace Markel.Pricing.Service.Infrastructure.Logging
{
    public class LoggingBehavior : BaseAspectBehavior, IDisposable
    {
        private ILogController logController;
        private IMethodReturn methodInvokeResult;
        internal const string MethodInfoFormatter = "Invoking method {0} at {1}";
        internal const string ExceptionFormatter = "Method {0} threw exception {1} at {2}";
        internal const string ReturnValueFormatter = "Method {0} returned {1} at {2}";

        public LoggingBehavior(ILogController log)
        {
            logController = log;
        }

        public override IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            if (logController != null)
            {
                logController.Log(new LogItem()
                {
                    Message = String.Format(MethodInfoFormatter, input.MethodBase, DateTime.Now.ToLongTimeString()),
                    LogLevel = LogLevel.Info
                });
            }

            methodInvokeResult = getNext()(input, getNext);

            if (methodInvokeResult.Exception != null)
            {
                if (logController != null)
                {
                    // Log Exception
                    logController.Log(new LogItem()
                    {
                        Message = String.Format(ExceptionFormatter, input.MethodBase, methodInvokeResult.Exception.Message, DateTime.Now.ToLongTimeString()),
                        Exception = methodInvokeResult.Exception,
                        LogLevel = LogLevel.Error
                    });
                }
            }
            else
            {
                if (logController != null)
                {
                    // Log Info about method return
                    logController.Log(new LogItem()
                    {
                        Message = String.Format(ReturnValueFormatter, input.MethodBase, methodInvokeResult.ReturnValue, DateTime.Now.ToLongTimeString()),
                        LogLevel = LogLevel.Info
                    });
                }
            }

            return methodInvokeResult;
        }

        #region Dispose

        private bool _disposed;

        ~LoggingBehavior()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            //if (disposing)
            //{
            //    // Free other managed objects that implement IDisposable only
            //    if (dependencyManager != null)
            //        dependencyManager.Dispose();
            //}

            // release any unmanaged objects
            // set the object references to null

            _disposed = true;
        }

        #endregion Dispose
    }
}