namespace CourierAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public Guid orderid { get; set; }

        public int frontpadid { get; set; }

        public int number { get; set; }

        [StringLength(50)]
        public string address { get; set; }

        public int price { get; set; }

        [StringLength(50)]
        public string phone { get; set; }

        public int code { get; set; }

        public int driver { get; set; }
    }
}
