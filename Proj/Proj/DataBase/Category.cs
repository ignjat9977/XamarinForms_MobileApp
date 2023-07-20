using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proj.DataBase
{
    [Table("Categories")]
    public class Category : Table
    {
        [NotNull, Unique]
        public string Name { get; set; }
    }
}
