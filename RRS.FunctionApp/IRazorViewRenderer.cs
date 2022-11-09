using System.Threading.Tasks;

namespace RRS.FunctionApp
{
    public interface IRazorViewRenderer
    {
        /// <summary>
        /// Renders a Razor view and model to a string.
        /// The name of the view is automatically inferred from the model name (e.g. IndexModel -> Index).
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<string> RenderViewToStringAsync<TModel>(TModel model);

        /// <summary>
        /// Renders a Razor view and model to a string.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<string> RenderViewToStringAsync<TModel>(TModel model, string viewName);
    }
}