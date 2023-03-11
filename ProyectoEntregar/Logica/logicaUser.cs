using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data.SQLite;
using System.Windows.Forms;
using ProyectoEntregar.Fisica;

namespace ProyectoEntregar.Logica
{

    public class LogicaUser
    {
        public static string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;
        private static LogicaUser _instanci = null;
        public LogicaUser() { }
        public static LogicaUser Instanci
        {
            get
            {
                if (_instanci == null)
                {
                    _instanci = new LogicaUser();
                }
                return _instanci;
            }
        }
        public bool Guardar(User obj)
        {
            bool respuesta = true;

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                conexion.Open();
                string query = "Insert INTO  User(Usuario,Password) values(@Usuario,@Password)";
                SQLiteCommand cmd = new SQLiteCommand(query, conexion);
                cmd.Parameters.Add(new SQLiteParameter("@Usuario", obj.Usuario));
                cmd.Parameters.Add(new SQLiteParameter("@Password", obj.Password));


                cmd.CommandType = System.Data.CommandType.Text;
                if (cmd.ExecuteNonQuery() < 1)
                {
                    respuesta = false;
                }

            }


            return respuesta;
        }

        public List<User> Listar()
        {
            List<User> listas = new List<User>();
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                conexion.Open();
                string query = "select distinct * from User ";
                SQLiteCommand cmd = new SQLiteCommand(query, conexion);
                cmd.CommandType = System.Data.CommandType.Text;
                using (SQLiteDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        listas.Add(new User()
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                             Usuario= (dr["Usuario"].ToString()),
                            Password = dr["Password"].ToString()
                         
                        });
                    }

                }

            }
return listas;

        }
        public List<User> Listar(string user, string passwd)
        {
            List<User> listas = new List<User>();
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                conexion.Open();
                string query = "select distinct * from User where Usuario=@User AND Password=@Password ";
                SQLiteCommand cmd = new SQLiteCommand(query, conexion);
                cmd.Parameters.Add(new SQLiteParameter("@User", user));
                cmd.Parameters.Add(new SQLiteParameter("@Password", passwd));

                cmd.CommandType = System.Data.CommandType.Text;
                using (SQLiteDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        listas.Add(new User()
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            Usuario = (dr["Usuario"].ToString()),
                            Password = dr["Password"].ToString()

                        });
                    }

                }

            }
            return listas;

        }
    }
}
