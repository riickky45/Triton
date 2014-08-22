using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace Acuanet
{
 
    //clase para leer configuraciones de un archivo XML
    class LecConfigXML
    {
        XmlDataDocument doc;
        XmlElement element;
        XmlNode node;
        string path;

        #region Constructor
        public LecConfigXML()
        {
            path = Application.StartupPath + "\\config.xml";
            doc = new XmlDataDocument();

            try
            {
                doc.Load(path);
                element = doc.DocumentElement;
            }
            catch
            {
            }
        }

        public LecConfigXML(string sarchivo)
        {

            path = Application.StartupPath + "\\"+sarchivo;
            doc = new XmlDataDocument();

            try
            {
                doc.Load(path);
                element = doc.DocumentElement;
            }
            catch
            {
            }
        }
        #endregion


        #region Properties
        public string Filename
        {
            set
            {
                path = value;
                doc.Load(path);
                element = doc.DocumentElement;
            }
            get { return path; }
        }
        #endregion

        //metodo para consultar una propiedad de Texto
        public string Text(string key, string defaultValue)
        {
            node = doc.SelectSingleNode(key);
            if (node == null)
                return defaultValue;
            else
                return node.InnerText;
        }

        //metodo para consultar una propiedad Entera (Int16)
        public int Int16(string key, int defaultValue)
        {
            int i;

            node = doc.SelectSingleNode(key);
            try
            {
                i = int.Parse(node.InnerText);
            }
            catch
            {
                return defaultValue;
            }
            return i;
        }


        //metodo para consultar una propiedad Double
        public double Double(string key, double defaultValue)
        {
            double i;

            node = doc.SelectSingleNode(key);
            try
            {
                i = double.Parse(node.InnerText);
            }
            catch
            {
                return defaultValue;
            }
            return i;
        }


        //metodo para consulta una propiedad booleana
        public bool Boolean(string key, bool defaultValue)
        {
            node = doc.SelectSingleNode(key);
            if (node == null)
            {
                return defaultValue;
            }

            if (node.InnerText.Equals("true", StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }

            return false;

        }

        //metodo para poner un valor a una propiedad 
        public bool Set(string key, string value)
        {
            node = doc.SelectSingleNode(key);
            if (node == null) return false;
            node.InnerText = value;
            return true;
        }

        //metodo para guarda el archivo de configuracion
        public void Save()
        {
            doc.Save(path);
        }
    }
}
