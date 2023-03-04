using System;
using System.Net.Http;
using System.Threading.Tasks;
using NodePreact.AspNetCore.ViewEngine;
using NodePreact.Utils;

namespace NodePreact.Components
{
    public sealed class ReactRouterComponent : ReactBaseComponent
    {
        public ReactRouterComponent(
            ReactConfiguration configuration,
            IReactIdGenerator reactIdGenerator,
            INodeInvocationService _nodeInvocationService,
            IComponentNameInvalidator componentNameInvalidator) : base(
            configuration,
            reactIdGenerator,
            _nodeInvocationService,
            componentNameInvalidator)
        {
        }

        public string Location { get; set; }
        
        public async Task<RoutingContext> RenderRouterWithContext()
        {
            if (ClientOnly)
            {
                return null;
            }

            try
            {
                var routingContext = await Render(new RenderOptions
                {
                    Location = Location,
                    DisableStreaming = true,
                    DisableBootstrapPropsInPlace = true,
                    BootstrapScriptContent = null,
                    ComponentName = ComponentName,
                    ServerOnly = ServerOnly,
                    Nonce = NonceProvider?.Invoke(),
                });

                OutputHtml = new PooledStream();
                await routingContext.CopyToStream(OutputHtml.Stream);

                return routingContext;
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex, ComponentName, ContainerId);
            }

            return null;
        }
    }
}
