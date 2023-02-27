using System;
using System.Windows;
using System.Xml;

namespace ProyectoEntregar
{
    class ConfiguracionDatos
    {
        public static int Size { get; set; } = 12;
        public static string Font { get; set; } = "Arial";
        public  static FontStyle Style { get; set; } = FontStyles.Normal;
        public  static FontWeight Weight { get; set; }=FontWeights.Normal;

        public ConfiguracionDatos() { } 
        
        public ConfiguracionDatos(int size, string font, FontStyle style,FontWeight weight) { Size = size; Font = font; Style = style;Weight = weight; }
    
        public static void LeerXML()
        {

            // Cargar el archivo XML
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("config.xml");

            // Obtener los valores de tamaño, fuente y estilo de fuente del archivo XML
            XmlNode sizeNode = xmlDocument.SelectSingleNode("/configuracion/Size");
            XmlNode fontNode = xmlDocument.SelectSingleNode("/configuracion/Font");
            XmlNode styleNode = xmlDocument.SelectSingleNode("/configuracion/Style");

            // Convertir los valores de tamaño y estilo a tipos numéricos
           Size = int.Parse(sizeNode.InnerText);
           Font = fontNode.InnerText;
         
            if (styleNode.InnerText.Contains("Italic"))
                Style = FontStyles.Italic;
            if (styleNode.InnerText.Contains("Bold"))
                Weight = FontWeights.Bold;


            muestraDatos();
        }
        public static void muestraDatos()
        {
            Console.WriteLine($"Tamaño de fuente: {Size}");
            Console.WriteLine($"Fuente: {Font}");
            Console.WriteLine($"Estilo de fuente: {Style}");
            Console.WriteLine($"Peso de fuente: {Weight}");

        }
    }
}
