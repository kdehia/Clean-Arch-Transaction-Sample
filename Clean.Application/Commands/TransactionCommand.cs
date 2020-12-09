using Clean.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Application.Commands
{



    public class TransactionCommand : IRequest<int>
    {
        public int number { get; set; } // it will be used to divide by it 
        

    }

    public class TransactionCommandHandler : IRequestHandler<TransactionCommand, int>
    {
        private readonly IDBContext context;

        public TransactionCommandHandler(IDBContext context)
        {
            this.context = context;
        }

        public async Task<int> Handle(TransactionCommand request, CancellationToken cancellationToken)
        {
            int updated = 0;
            await using var transaction = await context.datbase.BeginTransactionAsync();
            try
            {
                var blog = new Core.Entities.Blog { Url = $"Just test the number sent = {request.number}" };
                await context.Blogs.AddAsync(blog);
                await context.SaveChangesAsync(cancellationToken);

                for (int i = 0; i < 10; i++)
                {
                    var post = new Core.Entities.Post
                    {
                        BlogId = blog.BlogId,
                        Title = $" Title {i} for {blog.Url}"
                    };
                    await context.Posts.AddAsync(post); 
                    await context.SaveChangesAsync(cancellationToken);
                    updated++;
                }

                var divresult = 5 / request.number;
                await transaction.CommitAsync();
                
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return 0;
                
            }
            return updated;


        }
    }
}
