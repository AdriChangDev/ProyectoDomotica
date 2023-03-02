using System;
using System.Windows;

namespace ProyectoEntregar
{
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void IniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            login login = new login();
            login.Show();
            this.Close();

        }

        private void Configuracion_Click(object sender, RoutedEventArgs e)
        {
            Configuracion configuracion = new Configuracion();
            configuracion.ShowDialog();
        }

        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Saliendo...");
            Application.Current.Shutdown();
        }
        private void Registrarse_Click(object sender, RoutedEventArgs e)
        {
            register registrar=new register();
            registrar.ShowDialog();
            
        }
    }
}
