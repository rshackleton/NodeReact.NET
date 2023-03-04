using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Jering.Javascript.NodeJS;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NodePreact.Components;

namespace NodePreact
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNodePreact(this IServiceCollection services,
            Action<PreactConfiguration> configuration = null)
        {
            var config = new PreactConfiguration();
            configuration?.Invoke(config);

            services.AddSingleton(config);

            services.AddSingleton<IComponentNameInvalidator, ComponentNameInvalidator>();
            services.AddSingleton<IReactIdGenerator, PreactIdGenerator>();
            services.AddSingleton<INodeInvocationService, NodeInvocationService>();
            
            services.AddNodeJS();
            services.Configure<NodeJSProcessOptions>(options =>
            {
                options.EnvironmentVariables.Add("NODEPREACT_FILEWATCHERDEBOUNCE", config.FileWatcherDebounceMs.ToString());

                config.ConfigureNodeJSProcessOptionsAction?.Invoke(options);
            });
            services.Configure<OutOfProcessNodeJSServiceOptions>(options =>
            {
                options.Concurrency = Concurrency.MultiProcess;
                options.ConcurrencyDegree = config.EnginesCount;

                config.ConfigureOutOfProcessNodeJSServiceOptionsAction?.Invoke(options);
            });
            services.Configure<HttpNodeJSServiceOptions>(options =>
            {
                config.ConfigureHttpNodeJSServiceOptionsAction?.Invoke(options);
            });


            services.Replace(new ServiceDescriptor(
                typeof(IJsonService),
                typeof(NodePreactJeringNodeJsonService),
                ServiceLifetime.Singleton));

            services.AddScoped<IReactScopedContext, PreactScopedContext>();

            services.AddTransient<PreactComponent>();
            services.AddTransient<PreactRouterComponent>();

            return services;
        }
    }
}