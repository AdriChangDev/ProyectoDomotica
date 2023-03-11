using System.Windows;
using System.Windows.Forms;
using SQLite;

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
                System.Windows.Forms.MessageBox.Show("Las contraseñas no coinciden.");
                return;
            }
            else
            {
                User obj = new User()
                {
                    Usuario = usuario,
                    Password = contrasenia
                };
                bool resp = Logica.LogicaUser.Instanci.Guardar(obj);
                
                if (resp)
                {
                    System.Windows.Forms.MessageBox.Show("Usuario registrado correctamente.");
                    this.Close();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Usuario  NO  registrado.", "Registro Fallido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }


            }

            

        }

    }
}
