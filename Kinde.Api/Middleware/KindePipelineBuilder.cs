using System;

namespace Kinde.Auth.Middleware
{
    public sealed class KindePipelineBuilder
    {
        private readonly IList<Func<KindePipelineDelegate, KindePipelineDelegate>> _components =
            new List<Func<KindePipelineDelegate, KindePipelineDelegate>>();

        public KindePipelineBuilder Use(Func<KindePipelineDelegate, KindePipelineDelegate> middleware)
        {
            if (middleware == null)
                throw new ArgumentNullException(nameof(middleware));

            _components.Add(middleware);
            return this;
        }

        public KindePipelineDelegate Build()
        {
            KindePipelineDelegate pipeline = _ => Task.CompletedTask;

            for (var i = _components.Count - 1; i >= 0; i--)
            {
                pipeline = _components[i](pipeline);
            }

            return pipeline;
        }
    }
}