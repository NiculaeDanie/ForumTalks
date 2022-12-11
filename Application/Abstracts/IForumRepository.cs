using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstracts
{
    public interface IForumRepository
    {
        Task add(Forum forum);
        Task update(Forum forum);
        Task delete(Forum forum);
        Task<Forum> getById(string Id);
        Task<ICollection<Forum>> getAll();
    }
}
