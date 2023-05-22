using System;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace NodePreact.Benchmarks
{
	public class WebSimulateBenchmark : BaseBenchmark
	{
		private readonly NoTextWriter tk = new NoTextWriter(); //TODO: simulate PagedBufferedTextWriter

        [Benchmark]
        public async Task NodePreact_WebSimulation()
        {
            var tasks = Enumerable.Range(0, 20).Select(async x =>
            {
                using (var scope = sp.CreateScope())
                {
                    foreach (var ind in Enumerable.Range(0, 2))
                    {
                        var reactContext = scope.ServiceProvider.GetRequiredService<NodePreact.IReactScopedContext>();

                        var component = reactContext.CreateComponent<NodePreact.Components.PreactComponent>("__components.MovieAboutPage");
                        component.Props = _testData;

                        await component.RenderHtml();

                        component.WriteOutputHtmlTo(tk);
                    }

                    scope.ServiceProvider.GetRequiredService<NodePreact.IReactScopedContext>().GetInitJavaScript(tk);
                }
            });

            await Task.WhenAll(tasks);
        }
    }
}
