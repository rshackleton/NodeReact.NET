using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace NodePreact.Benchmarks
{
    public class SingleComponentBenchmark : BaseBenchmark
    {
        private readonly NoTextWriter tk = new NoTextWriter();

        [Benchmark]
        public async Task NodePreact_RenderRouterSingle()
        {
            using (var scope = sp.CreateScope())
            {
                var reactContext = scope.ServiceProvider.GetRequiredService<NodePreact.IReactScopedContext>();

                var component = reactContext.CreateComponent<NodePreact.Components.PreactRouterComponent>("__desktopComponents.App");
                component.Props = _testData;
                component.ServerOnly = true;
                component.Location = "/movie/246436/";

                await component.RenderRouterWithContext();

                component.WriteOutputHtmlTo(tk);
            }
        }

        [Benchmark]
        public async Task NodePreact_RenderSingle()
        {
            using (var scope = sp.CreateScope())
            {
                var reactContext = scope.ServiceProvider.GetRequiredService<NodePreact.IReactScopedContext>();

                var component = reactContext.CreateComponent<NodePreact.Components.PreactComponent>("__components.MovieAboutPage");
                component.Props = _testData;
                component.ServerOnly = true;

                await component.RenderHtml();

                component.WriteOutputHtmlTo(tk);
            }
        }
    }
}
