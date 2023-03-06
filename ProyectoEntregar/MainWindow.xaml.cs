using MaterialDesignThemes.Wpf;
using Microsoft.VisualStudio.Utilities.Internal;
using Newtonsoft.Json.Linq;
using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static SQLite.SQLite3;
using static SQLite.TableMapping;

namespace ProyectoEntregar
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string nombreHabitacion = "";
        private string user;
        private bool alertaMostrada = false;
        private bool alertaMostrada2 = false;

        Grid grid;

        private int contador = 1;



        public MainWindow(string usuario)
        {

            InitializeComponent();
            Closing += MainWindow_Closing; // suscribirse al evento Closing

            TControl.SelectionChanged += TControl_SelectionChanged;

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

                    alerta.Show();
                    this.Hide();

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
            usuarios.FontWeight = FontWeights.Bold;

            using (var db = new SQLiteConnection("database.db3"))
            {
                string query = "SELECT DISTINCT * FROM Relaciones where User=@user ";
                string hab = "A";
                var result = db.Query<Relaciones>(query, user);
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
                    ti.FontSize = ConfiguracionDatos.Size;
                    ti.IsSelected = true;
                    ti.FontFamily = new FontFamily(ConfiguracionDatos.Font);
                    ti.FontStyle = ConfiguracionDatos.Style;
                    ti.FontWeight = ConfiguracionDatos.Weight;
                    TControl.Items.Insert(contador - 1, ti);
                    contador++;

                    // Crear un Grid y agregarlo al contenido del TabItem
                }
                grid = new Grid();
                




            }




        }

       
        
            private void TControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
            var tabControl = (TabControl)sender;
            var tabItem = (TabItem)tabControl.SelectedItem;
            nombreHabitacion = tabItem.Header.ToString();

            // Crear un Grid para mostrar los dispositivos y el botón
            // Crear un Grid para mostrar los dispositivos y el botón
            var grid = new Grid();

            // Establecer la alineación del Grid para que se ajuste automáticamente al tamaño disponible
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;
            grid.VerticalAlignment = VerticalAlignment.Stretch;

            // Agregar una fila y una columna al Grid para el StackPanel
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            

            

            // Obtener la lista de dispositivos para esa habitación desde la base de datos
            var dispositivos = ObtenerDispositivosDeBaseDeDatos(nombreHabitacion);

            // Crear un StackPanel para mostrar los dispositivos
            var stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.Background = new SolidColorBrush(Colors.Aqua);

            // Crear un Border para cada dispositivo y agregarlo al StackPanel
            // Crear un Border para cada dispositivo
            foreach (var dispositivo in dispositivos)
            {
                if (dispositivo.Dispositivo.IsNullOrWhiteSpace() && dispositivo.HoraEncendido.IsNullOrWhiteSpace() && dispositivo.HoraApagado.IsNullOrWhiteSpace()) { }
                else
                {
                    var border = new Border();
                    border.Background = Brushes.White;
                    border.BorderThickness = new Thickness(1);
                    border.BorderBrush = Brushes.Black;
                    border.CornerRadius = new CornerRadius(5);
                    border.Padding = new Thickness(5);
                    border.Width = 200;
                    border.Height = 200;
                    border.HorizontalAlignment = HorizontalAlignment.Left;
                    border.VerticalAlignment = VerticalAlignment.Center;
                    border.Margin = new Thickness(10);

                    var nombreDispositivo = new TextBlock();
                    nombreDispositivo.Text = dispositivo.Dispositivo;
                    nombreDispositivo.HorizontalAlignment = HorizontalAlignment.Center;
                    nombreDispositivo.VerticalAlignment = VerticalAlignment.Center;

                    var horaEncendido = new TextBlock();
                    horaEncendido.Text = "Encendido: " + dispositivo.HoraEncendido.ToString();
                    horaEncendido.HorizontalAlignment = HorizontalAlignment.Center;
                    horaEncendido.VerticalAlignment = VerticalAlignment.Center;

                    var horaApagado = new TextBlock();
                    horaApagado.Text = "Apagado: " + dispositivo.HoraApagado.ToString();
                    horaApagado.HorizontalAlignment = HorizontalAlignment.Center;
                    horaApagado.VerticalAlignment = VerticalAlignment.Center;

                    // Agregar los TextBlocks al Border
                    border.Child = new StackPanel() { Children = { nombreDispositivo, horaEncendido, horaApagado } };

                    // Agregar el Border al Grid
                    stackPanel.Children.Add(border);
                }
            }

            // Agregar el StackPanel al Grid
            grid.Children.Add(stackPanel);
            Grid.SetRow(stackPanel, 0);
            Grid.SetColumn(stackPanel, 0);

            // Agregar un controlador de eventos para el botón
            var addButton = new Button();
            addButton.Content = "+";
            addButton.HorizontalAlignment = HorizontalAlignment.Right;
            addButton.VerticalAlignment = VerticalAlignment.Bottom;
            // Establecer el color de fondo para el estado normal y "hover"
            addButton.Background = new SolidColorBrush(Colors.LightGray);
            addButton.BorderBrush = new SolidColorBrush(Colors.Black);
            addButton.BorderThickness = new Thickness(1);

            // Establecer el tamaño y la posición del botón
            addButton.Width = 50;
            addButton.Height = 50;
            addButton.Margin = new Thickness(0, 0, 10, 10);

            //Agregar el botón al Grid en la segunda fila y segunda columna
            grid.Children.Add(addButton);
            Grid.SetRow(addButton, 1);
            Grid.SetColumn(addButton, 1);

            // Establecer el tamaño y la posición del botón
            addButton.Width = 50;
            addButton.Height = 50;
            addButton.Margin = new Thickness(0, 0, 10, 10);

            // Agregar un controlador de eventos para el botón
            addButton.Click += BtnAdd_Click;

            // Agregar el Grid al TabItem
            tabItem.Content = grid;



        }
        private List<Relaciones> ObtenerDispositivosDeBaseDeDatos(string nombreHabitacion)
        {

            var db = new SQLiteConnection("database.db3");

            string query = "SELECT * FROM Relaciones WHERE User = ? AND NombreHabitacion = ?";
            var result = db.Query<Relaciones>(query, user, nombreHabitacion);
            return result;
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!alertaMostrada2)
                {
                    AlertaDispositivo alert = new AlertaDispositivo(user, nombreHabitacion);

                    alert.Show();
                    this.Hide();


                    alertaMostrada2 = true;
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                this.Hide();
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

    

