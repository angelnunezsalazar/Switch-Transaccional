using System.Collections.Specialized;
using System.IO;
using System.Xml;

namespace SaveSettings
{
	public class Settings
	{

        enum OpcionesDireccion
        {
            DNS,
            IP
        }

		private static NameValueCollection settings;
		private static string path;
        
		static Settings()
		{
			path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
			path += @"\Settings.xml";
			
			if(!File.Exists(path))
				throw new FileNotFoundException(path + " could not be found.");

			System.Xml.XmlDocument xdoc = new XmlDocument();
			xdoc.Load(path);
			XmlElement root = xdoc.DocumentElement;
			System.Xml.XmlNodeList nodeList = root.ChildNodes.Item(0).ChildNodes;

			settings = new NameValueCollection();
            settings.Add("IP", nodeList.Item(0).Attributes["value"].Value);
            settings.Add("DNS", nodeList.Item(1).Attributes["value"].Value);
            settings.Add("Usar", nodeList.Item(2).Attributes["value"].Value);
            settings.Add("Puerto", nodeList.Item(3).Attributes["value"].Value);
            settings.Add("NumeroTransaccion", nodeList.Item(4).Attributes["value"].Value);
		}

		public static void Actualizar()
		{
			XmlTextWriter tw = new XmlTextWriter(path, System.Text.UTF8Encoding.UTF8);
			tw.WriteStartDocument();
			tw.WriteStartElement("configuration");
			tw.WriteStartElement("appSettings");

			for(int i=0; i<settings.Count; ++i)
			{
				tw.WriteStartElement("add");
				tw.WriteStartAttribute("key", string.Empty);
				tw.WriteRaw(settings.GetKey(i));
				tw.WriteEndAttribute();

				tw.WriteStartAttribute("value", string.Empty);
				tw.WriteRaw(settings.Get(i));
				tw.WriteEndAttribute();
				tw.WriteEndElement();
			}

			tw.WriteEndElement();
			tw.WriteEndElement();

			tw.Close();
		}

        public static int NumeroTransaccion
        {
            get { return int.Parse(settings.Get("NumeroTransaccion")); }
            set { settings.Set("NumeroTransaccion", value.ToString()); }
        }

		public static string DNS
		{
			get { return settings.Get("DNS"); }
			set { settings.Set("DNS", value); }
		}

        public static string IP
        {
            get { return settings.Get("IP"); }
            set { settings.Set("IP", value); }
        }

        public static bool UsarDNS
        {
            get {

                switch (settings.Get("Usar"))
                {
                    case "DNS":
                        return true;
                    case "IP":
                        return false;
                    default: return false;
                }
            }
            set { 
                
                settings.Set("Usar", value? "DNS":"IP"); 
            }
        }

		public static int Puerto
		{
			get { return int.Parse(settings.Get("Puerto")); }
			set { settings.Set("Puerto", value.ToString()); }
		}
	}
}
