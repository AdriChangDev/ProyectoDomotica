using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoEntregar
{
     class Habitaciones
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
        public string nombreHabitacion { get; set; }
         public Habitaciones() { }
        public Habitaciones( string nombreHabitacion)
        {
          
            this.nombreHabitacion = nombreHabitacion;
        }
    }
}
