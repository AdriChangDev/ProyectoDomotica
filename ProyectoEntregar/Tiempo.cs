using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoEntregar
{
    class Tiempo
    {
        public ObservableCollection<int> Horas { get; set; }
        public ObservableCollection<int> Minutos { get; set; }

        public Tiempo()
        {  // Llenar la lista de horas con valores de 0 a 23
            Horas = new ObservableCollection<int>();
            for (int i = 0; i < 24; i++)
            {
                Horas.Add(i);
            }

            // Llenar la lista de minutos con valores de 0 a 59
            Minutos = new ObservableCollection<int>();
            for (int i = 0; i < 60; i++)
            {
                Minutos.Add(i);
            }
        }
    }

}
