using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionOpenAPI.OpenApi
{
    public class OpenApiHttpTriggerAuthorization : DefaultOpenApiHttpTriggerAuthorization
    {
        public override Task<OpenApiAuthorizationResult> AuthorizeAsync(IHttpRequestDataObject req)
        {
            var result = default(OpenApiAuthorizationResult);
            var authtoken = (string)req.Headers["myAuthHeader"];

            if (authtoken.IsNullOrWhiteSpace())
            {
                result = new OpenApiAuthorizationResult()
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    ContentType = "text/plain",
                    Payload = "Header not exist",
                };

                return Task.FromResult(result);
            }

            if (!authtoken.Contains("max", StringComparison.CurrentCultureIgnoreCase))
            {
                result = new OpenApiAuthorizationResult()
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    ContentType = "text/plain",
                    Payload = "Header not valid",
                };

                return Task.FromResult(result);
            }

            return Task.FromResult(result);
        }

    }
}

