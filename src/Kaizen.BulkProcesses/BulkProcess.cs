using System.Net;
using Kaizen.EntityFrameworkCore;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Kaizen.BulkProcesses
{
    public class BulkProcess
    {
        private readonly ILogger _logger;
        private readonly KaizenDbContext _context;

        public BulkProcess(ILoggerFactory loggerFactory, KaizenDbContext context)
        {
            _logger = loggerFactory.CreateLogger<BulkProcess>();
            _context = context;
        }

        [Function("Create")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");


            return response;
        }
    }
}
