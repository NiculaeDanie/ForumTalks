using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Comment
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string ForumId { get; set; }
        public Forum Forum { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
