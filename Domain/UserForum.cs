using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class UserForum
    {
        public string UserId { get; set; }
        public User user { get; set; }
        public string ForumId { get; set; }
        public Forum Forum { get; set; }
    }
}
