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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProyectoEntregar
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Button cocina = new Button { Content = "Cocina" };
        private Button salon = new Button { Content = "Salon" };

        private List<string> cuartos = new List<string>();
        private List<Button> botones = new List<Button>();


        public MainWindow()
        {
            InitializeComponent();
            initialize();
        }

        private void initialize()
        {
            Title = "Título de la Interfaz";


            botones.Add(bt_habP);
            var banio = new Button { Content = "Baño" };
            botones.Add(banio);
            cuartos.Add(bt_habP.Content.ToString());
            cuartos.Add(banio.Content.ToString());

            var botonMas = new Button { Content = "+" };
            botonMas.Click += botonMas_Click;

            barraSuperior.Children.Add(bt_habP);
            barraSuperior.Children.Add(banio);
            barraSuperior.Children.Add(botonMas);

            var panelPrincipal = new StackPanel();
            panelPrincipal.Children.Add(barraSuperior);

            Content = panelPrincipal;
        }

        private void botonMas_Click(object sender, RoutedEventArgs e)
        {
            var nueva = new AddRoom(this);
            nueva.ShowDialog();

            if (nueva.seleccionado.Equals("cocina", StringComparison.OrdinalIgnoreCase))
            {
                if (YaExiste(nueva))
                {
                    MessageBox.Show("No puedes añadir eso", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    cuartos.Add(cocina.Content.ToString());
                    botones.Add(cocina);
                    barraSuperior.Children.Add(cocina);
                }
            }
            else if (nueva.seleccionado.Equals("salon", StringComparison.OrdinalIgnoreCase))
            {
                if (YaExiste(nueva))
                {
                    MessageBox.Show("No puedes añadir eso", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    cuartos.Add(salon.Content.ToString());
                    botones.Add(salon);
                    barraSuperior.Children.Add(salon);
                }
            }

        }

        private bool YaExiste(AddRoom nueva)
        {
            return cuartos.Contains(nueva.seleccionado);
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click()
        {

        }
    }
}
    

