using Clean.Application.Common;
using Clean.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Application.Queries
{
    public class BlogPostQuery : IRequest<QueryVM>
    {
    }

    public class BlogPostQueryHandler : IRequestHandler<BlogPostQuery, QueryVM>
    {
        private readonly IDBContext context;

        public BlogPostQueryHandler(IDBContext context)
        {
            this.context = context;
        }
        public async Task<QueryVM> Handle(BlogPostQuery request, CancellationToken cancellationToken)
        {
            var qvm = new QueryVM();
            var blogs = await context.Blogs.ToListAsync();
            var posts = await context.Posts.ToListAsync();
            foreach (var blog in blogs)
            {
                qvm.fullBlogs.Add(new FullBlogs
                {
                    Blogs = blog,
                    TotalPosts = posts.Where(p => p.BlogId == blog.BlogId).Count()
                    //Posts = posts.Where(p=>p.BlogId == blog.BlogId).ToList()
                    //blog.Posts.ToList()
                });
            }

            return qvm;
        }
    }

    public class QueryVM
    {
        public QueryVM()
        {
            fullBlogs = new List<FullBlogs>();
        }
        public List<FullBlogs> fullBlogs { get; set; }
    }

    public class FullBlogs {
        public FullBlogs()
        {
           // Posts = new List<Post>();
        }
        public Blog Blogs { get; set; }
        public int TotalPosts { get; set; }
        //public List<Post> Posts{ get; set; }
    }
}
