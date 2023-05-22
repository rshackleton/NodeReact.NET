using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using NodePreact.Components;

namespace NodePreact
{
    public interface IReactScopedContext
    {
        T CreateComponent<T>(string componentName) where T: PreactBaseComponent;

        void GetInitJavaScript(TextWriter writer);
    }

    public sealed class PreactScopedContext : IReactScopedContext
    {
        private readonly List<PreactBaseComponent> _components = new List<PreactBaseComponent>();

        private readonly IServiceProvider _serviceProvider;

        public PreactScopedContext(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T CreateComponent<T>(string componentName) where T: PreactBaseComponent
        {
            var component = _serviceProvider.GetRequiredService<T>();

            component.ComponentName = componentName;

            _components.Add(component);

            return component;
        }

        public void GetInitJavaScript(TextWriter writer)
        {
            foreach (var component in _components)
            {
                if (!component.ServerOnly)
                {
                    component.RenderJavaScript(writer);
                    writer.Write(';');
                }
            }
        }
    }
}
