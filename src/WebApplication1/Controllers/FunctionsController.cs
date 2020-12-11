using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/functions")]
    public class FunctionsController : Controller
    {
        private readonly IConfiguration _configuration;

        public FunctionsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            using var managementClient = new HttpClient {BaseAddress = new Uri("https://management.azure.com")};

            var subscription = _configuration["SubId"];
            const string rg = "HFLBrevprodRG";
            const string appName = "prod-hflbrevfunction";
            const string type = "functions";

            managementClient.DefaultRequestHeaders.Clear();
            managementClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _configuration["Token"]);

            var response = await managementClient.GetAsync($"subscriptions/{subscription}/resourceGroups/{rg}/providers/Microsoft.Web/sites/{appName}/{type}?api-version=2019-08-01");


            return await response.Content.ReadAsStringAsync();
        }
    }
}