using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RRS.FunctionApp.Views;

namespace RRS.FunctionApp
{
    public class SendRazorTemplate
    {
        private readonly IRazorViewRenderer _razorViewToStringRenderer;

        public SendRazorTemplate(IRazorViewRenderer razorViewToStringRenderer)
        {
            _razorViewToStringRenderer = razorViewToStringRenderer;
        }

        [FunctionName("SendRazorTemplate")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string name = req.Query["name"];

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name ??= data?.name;

            var model = new PocTemplateModel
            {
                Name = name
            };
            var htmlContent = await _razorViewToStringRenderer.RenderViewToStringAsync(model);

            log.LogInformation("C# HTTP trigger function processed a request.");
            return new ContentResult { Content = htmlContent, ContentType = "text/html" };
        }
    }
}
