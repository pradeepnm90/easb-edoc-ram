using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;
using System;
using System.Collections.Concurrent;

namespace Markel.Pricing.Service.Infrastructure.Dependency
{
    public class DisposeExtension : UnityContainerExtension, IDisposable
    {
        private DisposeStrategy strategy = new DisposeStrategy();

        protected override void Initialize()
        {
            Context.Strategies.Add(strategy, UnityBuildStage.TypeMapping);
        }

        public void Dispose()
        {
            strategy.Dispose();
            strategy = null;
        }

        class DisposeStrategy : BuilderStrategy, IDisposable
        {
            ConcurrentBag<IDisposable> disposables = new ConcurrentBag<IDisposable>();
            public override void PostBuildUp(IBuilderContext context)
            {
                if (context != null)
                {
                    IDisposable instance = context.Existing as IDisposable;
                    if (instance != null && !instance.GetType().Equals(typeof(UnityContainer)))
                    {
                        disposables.Add(instance);
                    }
                }
                base.PostBuildUp(context);
            }
            public void Dispose()
            {
                disposables.ForEach((item) =>
                {
                    if (item != null)
                    {
                        item.Dispose();
                        item = null;
                    }
                });
            }
        }
    }
}