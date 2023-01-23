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

namespace ProyectoEntregar
{
    public partial class AddRoom : Window
    {
        private List<string> lsitado = new List<string> { "Cocina", "Salon", "Terraza" };
        public string seleccionado;
        public AddRoom(Window frame)
        {
            Owner = frame;
            Title = "Adding Room";
            Width = 300;
            Height = 150;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            ResizeMode = ResizeMode.NoResize;
            var panel = new StackPanel();
            Content = panel;

            var botonOK = new Button { Content = "OK" };
            panel.Children.Add(botonOK);

            var panel_1 = new StackPanel();
            panel.Children.Add(panel_1);

            var etiqueta_1 = new Label { Content = "¿Que cuarto quiere añadir?" };
            panel_1.Children.Add(etiqueta_1);

            var spinner = new ComboBox();
            spinner.ItemsSource = lsitado;
            panel_1.Children.Add(spinner);

            botonOK.Click += (sender, e) =>
            {
                seleccionado = (string)spinner.SelectedValue;
                lsitado.Remove(seleccionado);
                Close();
            };
        }
    }
}

