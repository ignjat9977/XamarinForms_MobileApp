using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Proj.DataBase
{
    public class Db
    {
        private SQLiteConnection _con;
        public Db()
        {
            var dbPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dbFile = Path.Combine(dbPath, "proj.db3");

            _con = new SQLiteConnection(dbFile);
            _con.CreateTable<Product>();
            _con.CreateTable<Category>();
        }
        public SQLiteConnection Conn => _con;
    }
}
