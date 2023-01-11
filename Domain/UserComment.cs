using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class UserComment
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string? CommentId { get; set; }
        public Comment Comment { get; set; }
        public User User { get; set; }
    }
}
