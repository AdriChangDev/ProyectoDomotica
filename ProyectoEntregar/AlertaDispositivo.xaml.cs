using MaterialDesignThemes.Wpf;
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
using MessageBox = System.Windows.MessageBox;

namespace ProyectoEntregar
{
    /// <summary>
    /// Lógica de interacción para AlertaDispositivo.xaml
    /// </summary>
    public partial class AlertaDispositivo : Window
    {
        string user;
        string hab;

        List<string> listaDispositivosUsados;
        List<string> listaDispositivos;
        List<string> aux = new List<string>();

        public AlertaDispositivo(string usuario,string habitacion)
        {
            user=usuario;
            hab = habitacion;
            InitializeComponent();
            DataContext = new Tiempo();
            switch (habitacion)
            {
                case "Salon":
                    listaDispositivos = new List<string>();
                    listaDispositivos.Add("Bombilla");
                    listaDispositivos.Add("Asistente de voz");
                    listaDispositivos.Add("Televisor");
                    listaDispositivos.Add("Ventilador");
                    break;
                case "Habitacion Principal":
                    listaDispositivos = new List<string>();
                    listaDispositivos.Add("Bombilla");
                    listaDispositivos.Add("Termostato");
                    listaDispositivos.Add("Persiana");
                    listaDispositivos.Add("Televisor");
                    break;
                case "Habitacion  Invitado":
                    listaDispositivos = new List<string>();
                    listaDispositivos.Add("Bombilla");
                    listaDispositivos.Add("Termostato");
                    listaDispositivos.Add("Persiana");
                    break;
                case "Cocina":
                    listaDispositivos = new List<string>();
                    listaDispositivos.Add("Bombilla");
                    listaDispositivos.Add("Horno");
                    listaDispositivos.Add("Cafetera");
                    listaDispositivos.Add("Nevera");
                    break;
                case "Terraza":
                    listaDispositivos = new List<string>();
                    listaDispositivos.Add("Bombilla");
                    listaDispositivos.Add("Sistema de riego");
                    listaDispositivos.Add("Ventilador");
                    break;
                case "Baño":
                    listaDispositivos = new List<string>();
                    listaDispositivos.Add("Bombilla");
                    listaDispositivos.Add("Altavoz");
                    listaDispositivos.Add("Inodoro Inteligente");


                    break;
                default:
                    Console.WriteLine("Habitación no válida");
                    break;
            }
            using (var db = new SQLiteConnection("database.db3"))
            {
                string query = "SELECT DISTINCT * FROM Relaciones WHERE User=? AND NombreHabitacion=? ";
                List<Relaciones> result = db.Query<Relaciones>(query, user,hab);
                listaDispositivosUsados = new List<string>();
                foreach (Relaciones rel in result)
                {
                    listaDispositivosUsados.Add(rel.Dispositivo);
                }
            }
            for (int i = 0; i < listaDispositivos.Count; i++)
            {
                bool encontrada = false;
                for (int j = 0; j < listaDispositivosUsados.Count; j++)
                {
                    if (listaDispositivos[i].ToUpper() == listaDispositivosUsados[j].ToUpper())
                    {
                        encontrada = true;
                        break;
                    }
                }

                if (!encontrada)
                {
                    aux.Add(listaDispositivos[i]);
                }
            }
            if (aux.Count == 0)
            {
                MessageBox.Show("No se pueden añadir más elementos.", "Alerta de precaución", MessageBoxButton.OK, (MessageBoxImage)MessageBoxIcon.Warning);
                this.Close();
            }
            else
            {
                cmbDisp.SelectedIndex = 0;
                cmbDisp.ItemsSource = aux;
                cbxEncendidoHoras.SelectedIndex = 0;
                cbxEncendidoMinutos.SelectedIndex = 0;
                cbxApagadoHoras.SelectedIndex = 0;
                cbxApagadoMinutos.SelectedIndex = 1;


            }


        }
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            string horaInicio = string.Format("{0:D2}:{1:D2}", int.Parse(cbxEncendidoHoras.Text), int.Parse(cbxEncendidoMinutos.Text));
            string horaApagado = string.Format("{0:D2}:{1:D2}", int.Parse(cbxApagadoHoras.Text), int.Parse(cbxApagadoMinutos.Text));

            Relaciones rela=new Relaciones(hab,user,(string)cmbDisp.SelectedItem,horaInicio,horaApagado);
            MessageBox.Show("Añadido Correctamente","Alerta de añadido Correctamente", MessageBoxButton.OK, (MessageBoxImage)MessageBoxIcon.Information);
            var db = new SQLiteConnection("database.db3");
            db.Insert(rela);
            db.Close();
            MainWindow mw = new MainWindow(user);
            mw.Show();
            this.Close();





        }
    }
}
