using Application.Abstracts;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrrastructure.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _dataContext;
        public PostRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task add(Post post)
        {
            await _dataContext.Post.AddAsync(post);
        }

        public async Task delete(Post post)
        {
            _dataContext.Post.Remove(post);
        }

        public async Task<ICollection<Post>> getAll(string forumId)
        {
            return await _dataContext.Post.Where(post => post.ForumId == forumId).ToListAsync();
        }

        public async Task<Post> getById(string Id)
        {
            return await _dataContext.Post.Where(post => post.Id == Id).FirstOrDefaultAsync();
        }

        public async Task update(Post post)
        {
            _dataContext.Post.Update(post);
        }
    }
}
