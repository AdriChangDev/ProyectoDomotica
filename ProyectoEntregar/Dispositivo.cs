using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoEntregar
{
     class Dispositivo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string HoraApagado { get; set; }
        public string HoraEncendido { get; set; }
        public Dispositivo() { }
        public Dispositivo(int id, string nombre, string horaApagado, string horaEncendido)
        {
            Id = id;
            Nombre = nombre;
            HoraApagado = horaApagado;
            HoraEncendido = horaEncendido;
        }
    }
}
