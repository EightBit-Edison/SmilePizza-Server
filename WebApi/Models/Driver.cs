namespace WebApi
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Driver")]
    public partial class Driver
    {
        [Key]
        public Guid userid { get; set; }

        public int login { get; set; }

        [Required]
        [StringLength(50)]
        public string password { get; set; }
    }
}
