using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proj.DataBase
{
    public class Table
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
