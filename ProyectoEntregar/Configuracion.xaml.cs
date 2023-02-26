using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Xml;
using Button = System.Windows.Controls.Button;
using ComboBox = System.Windows.Controls.ComboBox;
using Label = System.Windows.Controls.Label;
using MessageBox = System.Windows.Forms.MessageBox;

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
            themeComboBox.Items.Add("10");
            themeComboBox.Items.Add("12");
            themeComboBox.Items.Add("14");
            themeComboBox.Items.Add("16");
            themeComboBox.Items.Add("18"); 
            themeComboBox.Items.Add("20");
            themeComboBox.Items.Add("22"); 
            themeComboBox.Items.Add("24");
            themeComboBox.Margin = new Thickness(10, 10, 0, 0);
            themeComboBox.Width = 70;
            themeComboBox.VerticalAlignment = VerticalAlignment.Top;
            themeComboBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
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
            fontComboBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            fontComboBox.SelectedIndex = 0;

            StyleComboBox=new ComboBox();
            StyleComboBox.Items.Add("Italic");
            StyleComboBox.Items.Add("Bold");
            StyleComboBox.Items.Add("Normal");
            StyleComboBox.Items.Add("Italic & Bold");
            StyleComboBox.Margin = new Thickness(10, 90, 0, 0);
            StyleComboBox.Width = 100;
            StyleComboBox.VerticalAlignment = VerticalAlignment.Top;
            StyleComboBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            StyleComboBox.SelectedIndex = 2;



            applyButton = new Button
            {
                Content = "Aplicar cambios",
                Margin = new Thickness(10, 160, 0, 0),
                Width = 80,
                Height = 20,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left
            };
            applyButton.Click += apply_Click;


            test = new Button
            {
                Content = "Testing",
                Margin = new Thickness(10, 90, 0, 0),
                Width = 80,
                Height = 20,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center
            };
            test.Click += test_Click;


            previewLabel = new Label();
            previewLabel.Content = "Texto de ejemplo";
            previewLabel.Margin = new Thickness(200, 10, 0, 0);
            previewLabel.Width = 300;
            previewLabel.Height = 200;
            previewLabel.Background = Brushes.White;
            previewLabel.BorderBrush = Brushes.Black;
            previewLabel.BorderThickness = new Thickness(1);

            // Agrega los controles al formulario

            this.Content = new Grid() { Children = { themeComboBox, fontComboBox,StyleComboBox,this.test, this.applyButton, this.previewLabel } };


        }

        // Evento que se dispara cuando se hace clic en el botón "Aplicar cambios"
        private void test_Click(object sender, RoutedEventArgs e)
        {
            // Obtén el tema y la fuente seleccionados
            string selectedTheme = (string)this.themeComboBox.SelectedItem;
            string selectedFont = (string)this.fontComboBox.SelectedItem;
            string styleFont = (string)this.StyleComboBox.SelectedItem;

            // Cambia el tema
            switch (selectedTheme)
            {
                case "10":
                   previewLabel.FontSize = 10; 
                    break;
                case "12":
                    previewLabel.FontSize = 12;
                    break;
                case "14":
                    previewLabel.FontSize = 14;           
                    break;
                case "16":
                    previewLabel.FontSize = 16;
                    break;
                case "18":
                    previewLabel.FontSize = 18;       
                    break;
                case "20":
                    previewLabel.FontSize = 20;
                    break;
                case "22":
                    previewLabel.FontSize = 22;   
                    break;
                case "24":
                    previewLabel.FontSize = 24;  
                    break;
                default:
                    break;
            }
            switch (styleFont)
            {
                case "Italic":
                    previewLabel.FontStyle = FontStyles.Italic;

                    break;
                case "Bold":
                    previewLabel.FontWeight = FontWeights.Bold;

                    break;
                case "Normal":
                    previewLabel.FontStyle = FontStyles.Normal;

                    break;
                case "Italic & Bold":
                    previewLabel.FontStyle = FontStyles.Italic;
                    previewLabel.FontWeight = FontWeights.Bold;

                    break;
                default:
                    // Acciones para el caso en que no se seleccione ninguna opción
                    break;
            }



            // Cambia la fuente
            if (selectedFont == "Arial")
            {
                previewLabel.FontFamily = new FontFamily("Arial");
            }
            else if (selectedFont == "Times New Roman")
            {
                previewLabel.FontFamily = new FontFamily("Times New Roman");
            }
            else if (selectedFont == "Courier New")
            {
                previewLabel.FontFamily = new FontFamily("Courier New");
            }
            else if (selectedFont == "Algerian")
            {
                previewLabel.FontFamily = new FontFamily("Algerian");
            }
            else if (selectedFont == "Consolas")
            {
                previewLabel.FontFamily = new FontFamily("Consolas");
            }

        }
        private void apply_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el tema, la fuente y el estilo seleccionados
            string selectedTheme = (string)themeComboBox.SelectedItem;
            string selectedFont = (string)fontComboBox.SelectedItem;
            string selectedStyle = (string)StyleComboBox.SelectedItem;

            // Crear el archivo de configuración XML
            using (XmlWriter writer = XmlWriter.Create("config.xml"))
            {
                // Escribir el encabezado del archivo XML
                writer.WriteStartDocument();

                // Escribir el elemento raíz
                writer.WriteStartElement("configuracion");

                // Escribir el elemento para el tamaño
                writer.WriteStartElement("Size");
                writer.WriteString(selectedTheme);
                writer.WriteEndElement();

                // Escribir el elemento para la fuente
                writer.WriteStartElement("Font");
                writer.WriteString(selectedFont);
                writer.WriteEndElement();

                // Escribir el elemento para el Style
                writer.WriteStartElement("Style");
                writer.WriteString(selectedStyle);
                writer.WriteEndElement();

                // Cerrar el elemento raíz
                writer.WriteEndElement();
                MessageBox.Show("Mensaje de OK", "Título del cuadro de mensaje", MessageBoxButtons.OK);

                // Cerrar el archivo XML
                writer.WriteEndDocument();
            }
        }



    }
}