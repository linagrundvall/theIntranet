using Microsoft.EntityFrameworkCore;
using theIntranet.Models;

namespace theIntranet.Data
{
    public class SqlContext : DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {

        }

        protected SqlContext()
        {

        }

        public virtual DbSet<CommentModel> Comments { get; set; }
    }
}
