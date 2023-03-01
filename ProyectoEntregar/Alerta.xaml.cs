using Microsoft.VisualBasic.ApplicationServices;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using MessageBox = System.Windows.MessageBox;

namespace ProyectoEntregar
{
    /// <summary>
    /// Lógica de interacción para Alerta.xaml
    /// </summary>
    public partial class Alerta : Window
    {
        public static bool vacio=false;
        string user;
        List<string> listahabitacionesOcupadas;
        List<string> listahabitaciones;
        List<string> aux = new List<string>();

        public Alerta(string usuario)
        {

            InitializeComponent();
            user = usuario;
            using (var db = new SQLiteConnection("database.db3"))
            {
                string query = "SELECT DISTINCT * FROM Relaciones WHERE User=@user ";
                List<Relaciones> result = db.Query<Relaciones>(query, user);
                listahabitacionesOcupadas = new List<string>();
                foreach (Relaciones rel in result)
                {
                    listahabitacionesOcupadas.Add(rel.NombreHabitacion);
                }
            }
            listahabitaciones = new List<string>();
            listahabitaciones.Add("Salon");
            listahabitaciones.Add("Habitacion Principal");
            listahabitaciones.Add("Habitacion Invitado");
            listahabitaciones.Add("Cocina");
            listahabitaciones.Add("Terraza");
            listahabitaciones.Add("Baño");
            for (int i = 0; i < listahabitaciones.Count; i++)
            {
                bool encontrada = false;
                for (int j = 0; j < listahabitacionesOcupadas.Count; j++)
                {
                    if (listahabitaciones[i].ToUpper() == listahabitacionesOcupadas[j].ToUpper())
                    {
                        encontrada = true;
                        break;
                    }
                }

                if (!encontrada)
                {
                    aux.Add(listahabitaciones[i]);
                }
            }
            if (aux.Count == 0) {
                MessageBox.Show("No se pueden añadir más elementos.", "Alerta de precaución", MessageBoxButton.OK, (MessageBoxImage)MessageBoxIcon.Warning);
                vacio = true;
                this.Close();
            }
            else 
            { 

            cmbHabitaciones.ItemsSource = aux;
            }
        }
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            string itemElegido=(string)cmbHabitaciones.SelectedItem;
            var db = new SQLiteConnection("database.db3");
            Relaciones relaciones = new Relaciones(itemElegido,user,"Prueba","00:00","00:00");
            db.Insert(relaciones);
            db.Close();
            MainWindow mainWindow = new MainWindow(user);
            mainWindow.Show();
            this.Close();


        }
    }
}
