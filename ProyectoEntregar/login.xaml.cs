using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SQLite;
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
        }

        private void EnviarButton_Click(object sender, RoutedEventArgs e)
        {
            string usuario = user.Text;
            string contrasenia = password.Password;
            Console.WriteLine("hola");
            using (  var db = new SQLiteConnection("database.db3"))
            {
                var query = $"SELECT * FROM user WHERE Usuario='{usuario}' AND password='{contrasenia}'";
                var result = db.Query<User>(query);

                if (result.Count > 0)
                {
                    // Si se encontró un registro con las credenciales ingresadas, mostrar mensaje de login correcto
                    MessageBox.Show("¡Login correcto!");
                    MainWindow  main= new MainWindow(usuario);
                    main.Show();
                    this.Hide();
                }
                else
                {
                    // Si no se encontró ningún registro, mostrar mensaje de error
                    MessageBox.Show("¡Error de login! Verifica tus credenciales e intenta nuevamente.");
                }
            }
        }

    }
}




