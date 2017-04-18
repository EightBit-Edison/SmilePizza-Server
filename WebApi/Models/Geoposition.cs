namespace WebApi
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Geoposition")]
    public partial class Geoposition
    {
        [Key]
        public Guid geoid { get; set; }

        public int driver { get; set; }

        
        [StringLength(50)]
        public string longitude { get; set; }

        
        [StringLength(50)]
        public string lattitude { get; set; }

        
        public DateTime tracktime { get; set; }
    }
}
