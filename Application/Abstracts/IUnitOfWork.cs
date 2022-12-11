using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstracts
{
    public interface IUnitOfWork: IDisposable
    {
        public IForumRepository ForumRepository { get; }
        public ICommentRepository CommentRepository { get; }
        public IPostRepository PostRepository { get; }
        Task Save();
    }
}
