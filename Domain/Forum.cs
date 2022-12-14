using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Forum
    {
        public string Id { get; set; }
        public string Name { get; set; }  
        public string Description { get; set; }
        public int Price { get; set; }
        public ICollection<UserForum> Users { get; set; }
        public ICollection<Post> Post { get; set; }
        public string OwnerId { get; set; }
    }
}
