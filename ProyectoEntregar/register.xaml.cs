using System.Windows;

namespace ProyectoEntregar
{
    public partial class register : Window
    {
        public register()
        {
            InitializeComponent();
        }

        private void BtnRegistro_Click(object sender, RoutedEventArgs e)
        {
            string usuario = TxtUsuario.Text;
            string contrasenia = PwbContrasenia.Password;
            string confirmacion = PwbConfirmacion.Password;

            if (contrasenia != confirmacion)
            {
                MessageBox.Show("Las contraseñas no coinciden.");
                return;
            }

            // Aquí iría el código para registrar al usuario en la base de datos o en algún archivo, etc.
            // Por ejemplo:
            // usuarioDAO.RegistrarUsuario(usuario, contrasenia);

            MessageBox.Show("Usuario registrado correctamente.");
        }

    }
}
