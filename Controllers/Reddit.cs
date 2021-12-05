using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace LeanCode_HomeProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Reddit : ControllerBase
    {
        [HttpGet("{subreddit}")]
        public async Task<ActionResult> GetAsync(string subreddit)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"https://reddit.com/r/{subreddit}/random.json");
                if (!response.IsSuccessStatusCode) return null; // Checks if we got a 200 OK

                var result = await response.Content.ReadAsStringAsync();
                JArray arr = JArray.Parse(result); // get a string from reddit api
                JObject jpost = JObject.Parse(arr[0]["data"]["children"][0]["data"].ToString()); // filter out metadata
                string url = jpost["url"].ToString(); // get the image url

                // RedditPost post = new RedditPost
                // {
                //     URL = url,
                //     RequestDate = DateTime.UtcNow
                // };

                return new JsonResult(url);

            }




        }

    }
}