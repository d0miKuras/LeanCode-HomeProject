using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeanCode_HomeProject
{
    [ApiController]
    [Route("[controller]")]
    public class History : ControllerBase
    {
        RequestDbContext _context;
        public History(RequestDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<RedditPost>> GetAsync()
        {
            var history = await _context.RedditPosts.ToArrayAsync();
            return history;
        }
    }
}