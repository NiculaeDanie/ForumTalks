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
        public ICollection<Forum> Forums { get; set; }
        public ICollection<Forum> OwnedForums { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
