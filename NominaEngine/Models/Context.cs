using Microsoft.EntityFrameworkCore;
using NominaEngine.Models.Entities;

namespace NominaEngine.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
    }
}
