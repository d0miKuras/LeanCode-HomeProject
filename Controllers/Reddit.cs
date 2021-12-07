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
                if (!response.IsSuccessStatusCode) return null; // Checks if we got a 200 OK.

                var result = await response.Content.ReadAsStringAsync();
                if (!result.StartsWith("[")) return new JsonResult("Subreddit does not exist!"); ;
                JArray arr = JArray.Parse(result); // Get a string from reddit api.
                JObject jpost = JObject.Parse(arr[0]["data"]["children"][0]["data"].ToString()); // Filter out metadata.
                string url = jpost["url"].ToString(); // Get the image url.

                RedditPost post = new RedditPost // Create an object to add to the database.
                {
                    URL = url,
                    RequestDate = DateTime.UtcNow,
                    Subreddit = subreddit
                };
                await _context.RedditPosts.AddAsync(post); // Add the created object.
                await _context.SaveChangesAsync(); // Save the database.

                return new JsonResult(url);
            }
        }
    }
}