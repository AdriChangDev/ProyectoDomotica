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

namespace ProyectoEntregar
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string nombreHabitacion="";
        private string user;
        private bool alertaMostrada = false;
        Grid grid;

        private int contador = 1;
     


        public MainWindow(string usuario)
        {
            
            InitializeComponent();
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
                        this.Close();
                    
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
            Grid gridContainer = null;
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
                    grid = new Grid();
                    grid.Background = new SolidColorBrush(Colors.Aqua);

                    ti.Content = grid;

                    // Código para crear el TabItem
                     nombreHabitacion = item;

                    query = "SELECT * FROM Relaciones WHERE User = ? AND NombreHabitacion = ?";
                    result = db.Query<Relaciones>(query, user, nombreHabitacion);

                    // Verificar si la habitación tiene al menos un dispositivo
                    bool tieneDispositivos = result.Any(so => !string.IsNullOrEmpty(so.Dispositivo) && !string.IsNullOrEmpty(so.HoraEncendido) && !string.IsNullOrEmpty(so.HoraApagado));

                    if (tieneDispositivos)
                    {
                        foreach (var so in result)
                        {
                            if (!string.IsNullOrEmpty(so.Dispositivo) && !string.IsNullOrEmpty(so.HoraEncendido) && !string.IsNullOrEmpty(so.HoraApagado))
                            {
                                Border border = new Border();
                                border.Background = Brushes.White;
                                border.BorderThickness = new Thickness(1);
                                border.BorderBrush = Brushes.Black;
                                border.CornerRadius = new CornerRadius(5);
                                border.Padding = new Thickness(5);
                                border.Width = 200;
                                border.Height = 200;
                                border.HorizontalAlignment = HorizontalAlignment.Left;
                                border.VerticalAlignment = VerticalAlignment.Top;

                                gridContainer = new Grid();
                                gridContainer.RowDefinitions.Add(new RowDefinition());
                                gridContainer.RowDefinitions.Add(new RowDefinition());

                                TextBlock textBlock1 = new TextBlock();
                                ConfiguracionDatos.LeerXML();
                                textBlock1.Text = so.Dispositivo;
                                textBlock1.HorizontalAlignment = HorizontalAlignment.Center;
                                textBlock1.FontSize = ConfiguracionDatos.Size;
                                textBlock1.FontFamily = new FontFamily(ConfiguracionDatos.Font);
                                textBlock1.FontStyle = ConfiguracionDatos.Style;
                                textBlock1.FontWeight = ConfiguracionDatos.Weight;
                                gridContainer.Children.Add(textBlock1);
                                Grid.SetRow(textBlock1, 0);

                                TextBlock textBlock2 = new TextBlock();
                                ConfiguracionDatos.LeerXML();
                                textBlock2.Text = so.HoraEncendido + "-" + so.HoraApagado;
                                textBlock2.HorizontalAlignment = HorizontalAlignment.Center;
                                textBlock2.FontSize = ConfiguracionDatos.Size;
                                textBlock2.FontFamily = new FontFamily(ConfiguracionDatos.Font);
                                textBlock2.FontStyle = ConfiguracionDatos.Style;
                                textBlock2.FontWeight = ConfiguracionDatos.Weight;
                                gridContainer.Children.Add(textBlock2);
                                Grid.SetRow(textBlock2, 1);

                                border.Child = gridContainer;
                                grid.Children.Add(border);
                                // Agregar el botón con el símbolo de suma al Grid
                                var addButton = new Button();
                                addButton.Content = new PackIcon { Kind = PackIconKind.Plus };
                                addButton.HorizontalAlignment = HorizontalAlignment.Right;

                                addButton.VerticalAlignment = VerticalAlignment.Bottom;
                                addButton.Content = "+";
                                // Establecer el color de fondo para el estado normal y "hover"
                                addButton.Background = new SolidColorBrush(Colors.Beige);

                                addButton.Width = 80;
                                addButton.Height = 80;

                                // Agregar un controlador de eventos para el botón
                                addButton.Click += BtnAdd_Click;
                                addButton.Margin = new Thickness(0, 0, 10, 10);




                                grid.Children.Add(addButton);

                            }
                          

                        }
                    }
                }
            }
          
           


        }
      
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("OK"+nombreHabitacion);
            AlertaDispositivo alert = new AlertaDispositivo(user,nombreHabitacion);
            alert.Show();
            this.Close();
        }
        private void TControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tabControl = (TabControl)sender;
            var tabItem = (TabItem)tabControl.SelectedItem;
            nombreHabitacion = tabItem.Header.ToString();
        }


    }
}

    

