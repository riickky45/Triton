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


        public string Text(string key, string defaultValue)
        {
            node = doc.SelectSingleNode(key);
            if (node == null)
                return defaultValue;
            else
                return node.InnerText;
        }

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

        public bool Set(string key, string value)
        {
            node = doc.SelectSingleNode(key);
            if (node == null) return false;
            node.InnerText = value;
            return true;
        }

        public void Save()
        {
            doc.Save(path);
        }
    }
}
