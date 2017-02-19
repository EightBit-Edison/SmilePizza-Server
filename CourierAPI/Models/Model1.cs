namespace CourierAPI
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Driver> Driver { get; set; }
        public virtual DbSet<Geoposition> Geoposition { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

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

            modelBuilder.Entity<Order>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.phone)
                .IsUnicode(false);
        }
    }
}
