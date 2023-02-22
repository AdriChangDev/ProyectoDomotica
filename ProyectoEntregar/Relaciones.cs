using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoEntregar
{
    class Relaciones
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string NombreHabitacion { get; set; }
        public string User { get; set; }
        public string Dispositivo { get; set; }
        public string HoraEncendido{ get; set; }
        public string HoraApagado { get; set; }
        public Relaciones() { }
        public Relaciones( string nombreHabitacion, string user, string dispositivo, string horaEncendido, string horaApagado)
        {
            this.NombreHabitacion = nombreHabitacion;
            this.User = user;
            this.Dispositivo = dispositivo;
            this.HoraEncendido = horaEncendido;
            this.HoraApagado = horaApagado;
        }
    }
}
