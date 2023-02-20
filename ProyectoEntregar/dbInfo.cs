using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ProyectoEntregar
{
     class dbInfo
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string User { get; set; }
        public string password { get; set; }
        dbInfo( string user, string password)
        {
            User = user;
            this.password = password;
        }
    }
}
