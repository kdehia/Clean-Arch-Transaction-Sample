using Clean.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Application.Common
{
    public interface IDBContext
    {
        DbSet<Blog> Blogs { get; set; }
        DbSet<Post> Posts{ get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        DatabaseFacade datbase { get; }
    }
}
