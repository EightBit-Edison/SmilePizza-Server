namespace WebApi
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBModel : DbContext
    {
        public DBModel()
            : base("name=DBModel")
        {
        }

        public virtual DbSet<Geoposition> Geopositions { get; set; }
        public virtual DbSet<Sm> Sms { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Geoposition>()
                .Property(e => e.longitude)
                .IsUnicode(false);

            modelBuilder.Entity<Geoposition>()
                .Property(e => e.lattitude)
                .IsUnicode(false);

            modelBuilder.Entity<Sm>()
                .Property(e => e.phone)
                .IsUnicode(false);
        }
    }
}
