namespace blogApp.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BlogContext : DbContext
    {
        public BlogContext()
            : base("name=BlogContext")
        {
        }

        public virtual DbSet<catalog> catalogs { get; set; }
        public virtual DbSet<news> news { get; set; }
        public virtual DbSet<user> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<catalog>()
                .HasMany(e => e.news)
                .WithOptional(e => e.catalog)
                .HasForeignKey(e => e.catId);
        }
    }
}
