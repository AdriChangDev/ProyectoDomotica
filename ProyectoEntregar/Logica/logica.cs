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
   
    public class Logica
    {
        public static string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;
        private static Logica _instanci = null;
        public Logica() { }
        public static Logica Instanci
        {
            get
            {
                if (_instanci == null)
                {
                    _instanci = new Logica();
                }
                return _instanci;
            }
        }
        public bool Guardar(Relaciones obj)
        {
            bool respuesta = true;

            using (SQLiteConnection conexion=new SQLiteConnection(cadena))
            {
                conexion.Open();
                string query = "Insert INTO  Relaciones(NombreHabitacion,User,Dispositivo,HoraEncendido,HoraApagado) values(@NombreHabitacion,@User,@Dispositivo,@HoraEncendido,@HoraApagado)";
                SQLiteCommand cmd=new SQLiteCommand(query, conexion);
                cmd.Parameters.Add(new SQLiteParameter("@NombreHabitacion", obj.NombreHabitacion));
                cmd.Parameters.Add(new SQLiteParameter("@User", obj.User));
                cmd.Parameters.Add(new SQLiteParameter("@Dispositivo", obj.Dispositivo));
                cmd.Parameters.Add(new SQLiteParameter("@HoraEncendido", obj.HoraEncendido));
                cmd.Parameters.Add(new SQLiteParameter("@HoraApagado", obj.HoraApagado));
                cmd.CommandType = System.Data.CommandType.Text;
                if (cmd.ExecuteNonQuery() < 1)
                {
                    respuesta = false;
                }

            }
           

                return respuesta;
        }

        public List<Relaciones> Listar(string user)
        {
             List<Relaciones> listas= new List<Relaciones>();
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                conexion.Open();
                string query = "select distinct * from Relaciones where User=@user";
                SQLiteCommand cmd= new SQLiteCommand(query, conexion);
                cmd.Parameters.Add(new SQLiteParameter("@user", user));
                cmd.CommandType=System.Data.CommandType.Text;
                using (SQLiteDataReader dr=cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        listas.Add(new Relaciones()
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            NombreHabitacion =(dr["NombreHabitacion"].ToString()),
                            Dispositivo = dr["Dispositivo"].ToString(),
                            HoraEncendido =dr["HoraEncendido"].ToString(),
                            HoraApagado = dr["HoraApagado"].ToString(),


                        });
                    }

                }
                return listas;

            }


        }
        public List<Relaciones> Listar(string user, string hab)
        {
            List<Relaciones> listas = new List<Relaciones>();
            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                conexion.Open();
                string query = "select distinct * from Relaciones where User=@user AND NombreHabitacion=@NombreHabitacion";
                SQLiteCommand cmd = new SQLiteCommand(query, conexion);
                cmd.Parameters.Add(new SQLiteParameter("@user", user));
                cmd.Parameters.Add(new SQLiteParameter("@NombreHabitacion", hab));

                cmd.CommandType = System.Data.CommandType.Text;
                using (SQLiteDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        listas.Add(new Relaciones()
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            NombreHabitacion = (dr["NombreHabitacion"].ToString()),
                            Dispositivo = dr["Dispositivo"].ToString(),
                            HoraEncendido = dr["HoraEncendido"].ToString(),
                            HoraApagado = dr["HoraApagado"].ToString(),


                        });
                    }

                }

            }

return listas;
        }
    }
}
