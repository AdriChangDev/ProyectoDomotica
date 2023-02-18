using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ProyectoEntregar
{
    public partial class Configuracion : Window
    {
        // Crea los controles de la ventana


        // Constructor de la clase Configuracion
        public Configuracion()
        {
            // Inicializa los controles de la ventana
            themeComboBox = new ComboBox();
            themeComboBox.Items.Add("Tema 1");
            themeComboBox.Items.Add("Tema 2");
            themeComboBox.Items.Add("Tema 3");
            themeComboBox.Margin = new Thickness(10, 10, 0, 0);
            themeComboBox.Width = 70;
            themeComboBox.VerticalAlignment = VerticalAlignment.Top;
            themeComboBox.HorizontalAlignment = HorizontalAlignment.Left;
            themeComboBox.SelectedIndex = 0;

            fontComboBox = new ComboBox();
            fontComboBox.Items.Add("Arial");
            fontComboBox.Items.Add("Times New Roman");
            fontComboBox.Items.Add("Courier New");
            fontComboBox.Items.Add("Algerian");
            fontComboBox.Items.Add("Consolas");
            fontComboBox.Margin = new Thickness(10, 50, 0, 0);
            fontComboBox.Width = 100;
            fontComboBox.VerticalAlignment = VerticalAlignment.Top;
            fontComboBox.HorizontalAlignment = HorizontalAlignment.Left;
            fontComboBox.SelectedIndex = 0;

            applyButton = new Button();
            applyButton.Content = "Aplicar cambios";
            applyButton.Margin = new Thickness(10, 90, 0, 0);
            applyButton.Width = 80;
            applyButton.Height = 20;
            applyButton.VerticalAlignment = VerticalAlignment.Top;
            applyButton.HorizontalAlignment = HorizontalAlignment.Left;

            applyButton.Click += new RoutedEventHandler(applyButton_Click);


            this.previewLabel = new Label();
            this.previewLabel.Content = "Texto de ejemplo";
            this.previewLabel.Margin = new Thickness(200, 10, 0, 0);
            this.previewLabel.Width = 300;
            this.previewLabel.Height = 200;
            this.previewLabel.Background = Brushes.White;
            this.previewLabel.BorderBrush = Brushes.Black;
            this.previewLabel.BorderThickness = new Thickness(1);

            // Agrega los controles al formulario

            this.Content = new Grid() { Children = { themeComboBox, fontComboBox, this.applyButton, this.previewLabel } };


        }

        // Evento que se dispara cuando se hace clic en el botón "Aplicar cambios"
        private void applyButton_Click(object sender, RoutedEventArgs e)
        {
            FontFamily fuenteSeleccionada = null;
            // Obtén el tema y la fuente seleccionados
            string selectedTheme = (string)this.themeComboBox.SelectedItem;
            string selectedFont = (string)this.fontComboBox.SelectedItem;

            // Cambia el tema
            if (selectedTheme == "Tema 1")
            {
                this.Background = Brushes.LightGray;
                this.Foreground = Brushes.Black;
            }
            else if (selectedTheme == "Tema 2")
            {
                this.Background = Brushes.Black;
                this.Foreground = Brushes.White;
            }
            else if (selectedTheme == "Tema 3")
            {
                this.Background = Brushes.White;
                this.Foreground = Brushes.Black;
            }

            // Cambia la fuente
            if (selectedFont == "Arial")
            {
                this.FontFamily = new FontFamily("Arial");
            }
            else if (selectedFont == "Times New Roman")
            {
                this.FontFamily = new FontFamily("Times New Roman");
            }
            else if (selectedFont == "Courier New")
            {
                this.FontFamily = new FontFamily("Courier New");
            }
            else if (selectedFont == "Algerian")
            {
                this.FontFamily = new FontFamily("Algerian");
            }
            else if (selectedFont == "Consolas")
            {
                this.FontFamily = new FontFamily("Consolas");
            }

        }
    }
}