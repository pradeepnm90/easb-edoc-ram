using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.Dependency
{
    public abstract class BaseAspectBehavior : IInterceptionBehavior
    {
        internal bool WillExecuteBehavior = true;

        public abstract IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext);

        public virtual IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        /// <summary>
        /// Denotes if the interception behavior will execute, true by default unless overriden
        /// </summary>
        public virtual bool WillExecute
        {
            get { return WillExecuteBehavior; }
            set { WillExecuteBehavior = value; }
        }
    }
}
