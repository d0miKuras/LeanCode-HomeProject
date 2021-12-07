using System;
using Microsoft.EntityFrameworkCore;

namespace LeanCode_HomeProject
{
    public class RequestDbContext : DbContext
    {
        public RequestDbContext(DbContextOptions<RequestDbContext> options) : base(options) { }
        public DbSet<RedditPost> RedditPosts { get; set; }
    }
}