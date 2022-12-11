using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstracts
{
     public interface IPostRepository
    {
        Task add(Post post);
        Task update(Post post);
        Task delete(Post post);
        Task<Post> getById(string Id);
        Task<ICollection<Post>> getAll(string forumId);
    }
}
