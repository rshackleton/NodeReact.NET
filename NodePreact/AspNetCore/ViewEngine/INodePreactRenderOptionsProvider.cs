using Microsoft.AspNetCore.Mvc.Rendering;
using NodePreact.Components;

namespace NodePreact.AspNetCore.ViewEngine;

/// <summary>
/// INodePreactViewOptionsProvider provider is called on each rendering of a react component from NodePreactViewEngine.
/// </summary>
public interface INodePreactRenderOptionsProvider
{
    /// <summary>
    /// Provide node react direct streaming options.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="componentName"></param>
    /// <returns></returns>
    RenderOptions Provide(ViewContext context, string componentName);
}
