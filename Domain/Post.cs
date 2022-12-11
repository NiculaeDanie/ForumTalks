using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Post
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
        public string ForumId { get; set; }
        public Forum Forum { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public string OwnerId { get; set; }
    }
}
