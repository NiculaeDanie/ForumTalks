using Application.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;
        public IForumRepository ForumRepository { get; private set; }

        public ICommentRepository CommentRepository { get; private set; }

        public IPostRepository PostRepository { get; private set; }

        public UnitOfWork(DataContext dataContext, IForumRepository forumRepository, ICommentRepository commentRepository, IPostRepository postRepository)
        {
            _dataContext = dataContext;
            ForumRepository = forumRepository;
            CommentRepository = commentRepository;
            PostRepository = postRepository;
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }

        public async Task Save()
        {
            await _dataContext.SaveChangesAsync();
        }
    }
}
