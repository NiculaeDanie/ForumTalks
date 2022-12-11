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
    public class ForumRepository : IForumRepository
    {
        private readonly DataContext _context;
        public ForumRepository(DataContext context)
        {
            _context = context;
        }

        public async Task add(Forum forum)
        {
            await _context.Forum.AddAsync(forum);
        }

        public async Task delete(Forum forum)
        {
            _context.Forum.Remove(forum);
        }

        public async Task<ICollection<Forum>> getAll()
        {
            return await _context.Forum.Include(x => x.Users).ToListAsync();
        }

        public async Task<Forum> getById(string id)
        {
            return await _context.Forum.Where(forum => forum.Id == id).Include(x=> x.Users).FirstOrDefaultAsync();
        }

        public async Task update(Forum forum)
        {
            _context.Forum.Update(forum);
        }
    }
}
