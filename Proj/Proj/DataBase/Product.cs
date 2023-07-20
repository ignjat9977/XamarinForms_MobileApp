using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proj.DataBase
{
   
    [Table("Products")]
    public class Product : Table
    {
        [Unique, NotNull]
        public string Name { get; set; }
        public string Description { get; set; }
        [NotNull]
        public decimal? Price { get; set; }
        [NotNull]
        public int CategoryId { get; set; }
        public string Image { get; set; }
    }
}
