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
using System.Xml;
using SQLite;
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
            Console.WriteLine("hola");
            using (var db = new SQLiteConnection("database.db3")
)
            {
                var query = $"SELECT * FROM user WHERE Usuario='{usuario}' AND password='{contrasenia}'";
                var result = db.Query<User>(query);

                if (result.Count > 0)
                {
                    // Si se encontró un registro con las credenciales ingresadas, mostrar mensaje de login correcto
                    MessageBox.Show("¡Login correcto!","Login",MessageBoxButton.OK,(MessageBoxImage)MessageBoxIcon.Information);
                    
                    MainWindow  main= new MainWindow(usuario);
               
                    main.ShowDialog();
                    this.Hide();
                }
                else
                {
                    // Si no se encontró ningún registro, mostrar mensaje de error
                    MessageBox.Show("¡Error de login! Verifica tus credenciales e intenta nuevamente.","Error",MessageBoxButton.OK, (MessageBoxImage)MessageBoxIcon.Error);
                }
            }
        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (MessageBox.Show("¿Está seguro de quieres cerrar sesion?", "Confirmación", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                Window1 w1 = new Window1();
                w1.Show();
            }
        }


    }
}




