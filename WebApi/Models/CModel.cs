namespace WebApi
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CModel : DbContext
    {
        public CModel()
            : base("name=CModel")
        {
        }

        public virtual DbSet<Driver> Driver { get; set; }
        public virtual DbSet<Geoposition> Geoposition { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Driver>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<Geoposition>()
                .Property(e => e.longitude)
                .IsUnicode(false);

            modelBuilder.Entity<Geoposition>()
                .Property(e => e.lattitude)
                .IsUnicode(false);

        }
    }
}
