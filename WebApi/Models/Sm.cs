namespace WebApi
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int orderid { get; set; }

        [Required]
        [StringLength(50)]
        public string phone { get; set; }
    }
}
