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
        string[] habitaciones = new string[] { "Habitacion Invitado", "Terraza", "Cocina", "Salon" };

        private int contador = 1;
     


        public MainWindow(string usuario)
        {
            
            InitializeComponent();
            user= usuario;
            
           
        }


        private void TabItem_GotFocus(object sender, RoutedEventArgs e)
        {

            System.Windows.Forms.ComboBox comboBox = new System.Windows.Forms.ComboBox();
            comboBox.Items.AddRange(habitaciones);

            System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show(comboBox, "Seleccione la habitación:", "Añadir habitación", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string nombre = comboBox.SelectedItem as string;
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    System.Windows.MessageBox.Show("Debe seleccionar una habitación.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    contador = TControl.Items.Count;
                    TabItem ti = new TabItem { Header = nombre };
                    ti.FontSize = 22;
                    ti.IsSelected = true;
                    ti.FontFamily = new FontFamily("Bernard MT Condensed");
                    TControl.Items.Insert(contador - 1, ti);
                    contador++;
                }
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock usuarios = (TextBlock)FindName("usuarios");
            usuarios.Text = user;
            usuarios.FontWeight= FontWeights.Bold;
            using (var db = new SQLiteConnection("database.db3"))
            {
                string query = "SELECT DISTINCT * FROM Relaciones WHERE User=@user";
                var result = db.Query<Relaciones>(query,user);
                foreach (var item in result)
                {
                    contador = TControl.Items.Count;
                    TabItem ti = new TabItem { Header = item.NombreHabitacion };
                    ti.FontSize = 22;
                    ti.IsSelected = true;
                    ti.FontFamily = new FontFamily("Bernard MT Condensed");
                    TControl.Items.Insert(contador - 1, ti);
                    contador++;
                    // Código para crear el TabItem
/*
                    // Crear el Grid
                    Grid grid = new Grid();
                    grid.ColumnDefinitions.Add(new ColumnDefinition());
                    grid.ColumnDefinitions.Add(new ColumnDefinition());

                    // Agregar los encabezados de las columnas
                    Label nombreLabel = new Label() { Content = "Nombre del Dispositivo" };
                    Grid.SetColumn(nombreLabel, 0);
                    grid.Children.Add(nombreLabel);

                    // Obtener los dispositivos conectados
                    using (var db2 = new SQLiteConnection("database.db3"))
                    {
                        string query2 = "SELECT NombreDispositivo FROM Dispositivos WHERE NombreHabitacion=@nombreHabitacion";
                        var result2 = db.Query<string>(query, new { nombreHabitacion = nombre });

                        int row = 1;
                        foreach (var dispositivo in result)
                        {
                            // Agregar el nombre del dispositivo al Grid
                           Label nombreDispositivoLabel = new Label() { Content = dispositivo };
                            Grid.SetRow(nombreDispositivoLabel, row);
                            Grid.SetColumn(nombreDispositivoLabel, 0);
                            grid.Children.Add(nombreDispositivoLabel);

                            row++;
                        }
                    }

                    // Agregar el Grid al contenido del TabItem
                    ti.Content = grid;

                    // Agregar el TabItem a la lista de TabItems
                    TControl.Items.Insert(contador - 1, ti);
                    contador++;*/
                }


            }
        }

                

    }




}

    

