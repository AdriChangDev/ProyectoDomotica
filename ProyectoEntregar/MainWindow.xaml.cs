using SQLite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Button = System.Windows.Controls.Button;
using ComboBox = System.Windows.Controls.ComboBox;
using Label = System.Windows.Controls.Label;

namespace ProyectoEntregar
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string user;
        private bool alertaMostrada = false;

        private int contador = 1;
     


        public MainWindow(string usuario)
        {
            
            InitializeComponent();
            user = usuario;
            ConfiguracionDatos.LeerXML();
            tabItem.FontFamily = new FontFamily(ConfiguracionDatos.Font);
            tabItem.FontSize = ConfiguracionDatos.Size;
            tabItem.FontStyle = ConfiguracionDatos.Style;
            tabItem.FontWeight = ConfiguracionDatos.Weight;
            


        }


        private void TabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!alertaMostrada)
                {
                    Alerta alerta = new Alerta(user);
                    if (alerta == null)
                    {
                        alerta.ShowDialog();
                        this.Close();
                    }
                    alertaMostrada = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock usuarios = (TextBlock)FindName("usuarios");
            usuarios.Text = user;
            ConfiguracionDatos.LeerXML();
            usuarios.FontWeight= FontWeights.Bold;
            using (var db = new SQLiteConnection("database.db3"))
            {
                string query = "SELECT DISTINCT * FROM Relaciones where User=@user ";
                string hab="A";
                var result = db.Query<Relaciones>(query,user);
                List<string> aux = new List<string>();
               for (int i = 0; i < result.Count; i++)
                {
                    if (hab.ToUpper() != result[i].NombreHabitacion.ToUpper())
                    {
                        hab = result[i].NombreHabitacion;
                        aux.Add(hab);
                    }
                } 




                foreach (var item in aux)
                {
                    contador = TControl.Items.Count;
                    TabItem ti = new TabItem { Header = item };
                    ti.FontSize =ConfiguracionDatos.Size ;
                    ti.IsSelected = true;
                    ti.FontFamily =new FontFamily(ConfiguracionDatos.Font);
                    ti.FontStyle=ConfiguracionDatos.Style;
                    ti.FontWeight= ConfiguracionDatos.Weight;
                    TControl.Items.Insert(contador - 1, ti);
                    contador++;
                    // Código para crear el TabItem
                    string nombreHabitacion = item;

                    query = "SELECT * FROM Relaciones WHERE User = ? AND NombreHabitacion = ?";
                    result = db.Query<Relaciones>(query, user, nombreHabitacion);

                    foreach (var so in result)
                    {
                        Console.WriteLine(so.NombreHabitacion.ToString());

                    }

                }


            }
        }

                

    }




}

    

