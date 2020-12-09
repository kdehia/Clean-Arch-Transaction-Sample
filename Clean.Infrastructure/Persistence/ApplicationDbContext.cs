using Clean.Application.Common;
using Clean.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IDBContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {

        }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public DatabaseFacade datbase => Database;


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            
            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
