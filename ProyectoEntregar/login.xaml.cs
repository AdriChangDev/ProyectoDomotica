using System;

using System.Windows;

using System.Windows.Forms;

using MessageBox = System.Windows.MessageBox;

namespace ProyectoEntregar
{
    /// <summary>
    /// Lógica de interacción para login.xaml
    /// </summary>
    public partial class login : Window
    {
        public login()
        {
            InitializeComponent();
            Closing += MainWindow_Closing; // suscribirse al evento Closing

        }

        private void EnviarButton_Click(object sender, RoutedEventArgs e)
        {
            string usuario = user.Text;
            string contrasenia = password.Password;




            var result = Logica.LogicaUser.Instanci.Listar(usuario,contrasenia);

                if (result.Count > 0)
                {
                    // Si se encontró un registro con las credenciales ingresadas, mostrar mensaje de login correcto
                    MessageBox.Show("¡Login correcto!","Login",MessageBoxButton.OK,(MessageBoxImage)MessageBoxIcon.Information);
                    
                    MainWindow  main= new MainWindow(usuario);
                    main.Owner = this;
                    main.Show();
                    this.Hide();
                }
                else
                {
                    // Si no se encontró ningún registro, mostrar mensaje de error
                    MessageBox.Show("¡Error de login! Verifica tus credenciales e intenta nuevamente.","Error",MessageBoxButton.OK, (MessageBoxImage)MessageBoxIcon.Error);
                }
            
        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Owner.Show();
        }


    }
}




