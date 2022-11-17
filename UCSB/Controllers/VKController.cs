using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using UCCS.Models;

namespace UCSB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VKController : ControllerBase
    {
        private readonly string access_token = "4893f3db4893f3db4893f3db524b828443448934893f3db2be1b780519f00940de4665d";
        private readonly ILogger<VKController> _logger;

        public VKController(ILogger<VKController> logger)
        {
            _logger = logger;
        }

        [HttpGet("[action]")]
        //[HttpGet("[action]/{domain}")]
        public async Task<IActionResult> GetPosts(/*string domain*/)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var domain = "durov";

                    client.BaseAddress = new Uri("https://api.vk.com");
                    var response = await client.GetAsync($"/method/wall.get?domain={domain}&count=5&access_token={access_token}&v=5.131");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawWeather = JsonConvert.DeserializeObject<VKWall>(stringResult);

                    var res = rawWeather.Response.Items.Aggregate(
                        new StringBuilder(),
                        (current, next) => current.Append(next.Text))
                        .ToString().Replace(" ", "").ToLower();

                    CountToDB.LettersToDB(res, domain, _logger);

                    return Ok(new
                    {
                        Text = rawWeather.Response
                    });
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest(httpRequestException.Message);
                }
            }
        }
    }
}