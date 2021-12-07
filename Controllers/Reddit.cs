using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace LeanCode_HomeProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Random : ControllerBase
    {
        IConfiguration _config;
        RequestDbContext _context;

        public Random(IConfiguration config, RequestDbContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string subreddit = _config.GetValue<string>("Subreddit");
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

                RedditPost post = new RedditPost
                {
                    URL = url,
                    RequestDate = DateTime.UtcNow,
                    Subreddit = subreddit
                };
                await _context.RedditPosts.AddAsync(post);
                await _context.SaveChangesAsync();

                return new JsonResult(url);

            }




        }

    }
}