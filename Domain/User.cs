using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class User: IdentityUser
    {
        public ICollection<UserForum> Forums { get; set; }
        public ICollection<Forum> OwnedForums { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<UserComment> LikedComments { get; set; }
        public string Description { get; set; }
    }
}
