using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoEntregar.Fisica
{
    public class Relaciones
    {

        public int Id { get; set; }
        public string NombreHabitacion { get; set; }
        public string User { get; set; }
        public string Dispositivo { get; set; }
        public string HoraEncendido{ get; set; }
        public string HoraApagado { get; set; }
       
    }
}
