using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstracts
{
    public interface ICommentRepository
    {
        Task add(Comment comment);
        Task update(Comment comment);
        Task delete(Comment comment);
        Task<Comment> getById(string Id);
        Task<ICollection<Comment>> getAll(string postId);
    }
}
