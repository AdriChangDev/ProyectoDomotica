using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoEntregar
{
     class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Unique]
        public string Usuario { get; set; }
        public string Password { get; set; }

        public User() { }
        public User( string usuario, string password)
        {
            Usuario = usuario;
            Password = password;
        }
    }
}
