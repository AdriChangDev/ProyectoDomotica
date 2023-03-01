using System.Windows;
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
            if (System.IO.File.Exists("database.db3")) { } 
            else
            {
                System.IO.File.Create("database.db3");
                var db = new SQLiteConnection("database.db3");
                db.CreateTable<User>();
                db.CreateTable<Relaciones>();
                db.Close();
            }
            string usuario = TxtUsuario.Text;
            string contrasenia = PwbContrasenia.Password;
            string confirmacion = PwbConfirmacion.Password;

            if (contrasenia != confirmacion)
            {
                MessageBox.Show("Las contraseñas no coinciden.");
                return;
            }
            else
            {
                User user=new User(usuario,contrasenia);
                MessageBox.Show("Usuario registrado correctamente.");
                var db = new SQLiteConnection("database.db3");
                db.Insert(user);
                db.Close();
                Close();
            }

            

        }

    }
}
