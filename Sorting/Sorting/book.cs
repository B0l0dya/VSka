namespace Sorting
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Book
    {
        public int id { get; set; }

        [StringLength(50)]
        public string title { get; set; }

        public int pages { get; set; }

        [Column(TypeName = "money")]
        public decimal cost { get; set; }
    }
}
