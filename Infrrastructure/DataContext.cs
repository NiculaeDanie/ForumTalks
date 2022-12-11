using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrrastructure
{
    public class DataContext: IdentityDbContext<User>
    {
        public DataContext()
        {

        }
        public DbSet<Forum> Forum { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Post> Post { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) => builder.UseSqlServer(@"Data Source=DESKTOP-MHJDP0S\SQLEXPRESS;Initial Catalog=ForumTalks;Trusted_Connection=True;TrustServerCertificate=True;");


    }
}
