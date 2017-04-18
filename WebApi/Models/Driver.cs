namespace WebApi
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.IO;
    using AngleSharp;
    using AngleSharp.Dom;
    using AngleSharp.Parser.Html;

    
    public partial class Driver
    {
    
        public string login { get; set; }
        public string password { get; set; }
        public string name { get; set; }

    }
}
