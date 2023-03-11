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
using ProyectoEntregar.Fisica;
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

        public AlertaDispositivo(string usuario, string habitacion)
        {
            user = usuario;
            hab = habitacion;
            InitializeComponent();
            Closing += MainWindow_Closing; // suscribirse al evento Closing
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
                case "Habitacion Invitado":
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

            List<Relaciones> result = Logica.Logica.Instanci.Listar(user, hab); 
                listaDispositivosUsados = new List<string>();
                foreach (Relaciones rel in result)
                {
                    listaDispositivosUsados.Add(rel.Dispositivo);
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
                this.Owner.Show();
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
            Boolean adding = false;
            string horaInicio = string.Format("{0:D2}:{1:D2}", int.Parse(cbxEncendidoHoras.Text), int.Parse(cbxEncendidoMinutos.Text));
            string horaApagado = string.Format("{0:D2}:{1:D2}", int.Parse(cbxApagadoHoras.Text), int.Parse(cbxApagadoMinutos.Text));

            Relaciones rela=new Relaciones()
            {
                NombreHabitacion= hab,
                User=user,
                Dispositivo=(string)cmbDisp.SelectedItem,
                 HoraEncendido=horaInicio,
                HoraApagado= horaApagado

            };
            if (rela.Dispositivo.Equals(null))
            {
            }
            else {
                adding = Logica.Logica.Instanci.Guardar(rela);
            }
            if (adding)
            {
                MessageBox.Show("Añadido Correctamente", "Alerta de añadido Correctamente", MessageBoxButton.OK, (MessageBoxImage)MessageBoxIcon.Information);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No se puede añadir", "Añadir Fallido", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            this.Owner.Show();
            this.Close();





        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Owner.Show();

        }
    }
}
