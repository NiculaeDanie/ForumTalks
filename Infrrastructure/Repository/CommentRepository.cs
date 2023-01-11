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
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _dataContext;
        public CommentRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task add(Comment comment)
        {
            await _dataContext.Comment.AddAsync(comment);
        }

        public async Task delete(Comment comment)
        {
            _dataContext.Comment.Remove(comment);
        }

        public async Task<ICollection<Comment>> getAll(string postId)
        {
            return await _dataContext.Comment.Where(comment => comment.PostId == postId).Include(x=> x.LikedUsers).ToListAsync();
        }

        public async Task<Comment> getById(string Id)
        {
            return await _dataContext.Comment.Where(comment => comment.Id == Id).Include(x=> x.LikedUsers).FirstOrDefaultAsync();
        }

        public async Task update(Comment comment)
        {
            _dataContext.Comment.Update(comment);
        }
    }
}
