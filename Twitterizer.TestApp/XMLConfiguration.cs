using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace Twitterizer.TestApp
{
    public static class XMLConfiguration
    {
        public static void Save(string UserName, string Password, string Path)
        {
            XmlDocument ConfigDocument = new XmlDocument();
            XmlElement ConfigElement = ConfigDocument.CreateElement("config");

            XmlElement newElement = ConfigDocument.CreateElement("username");
            newElement.InnerText = UserName;
            ConfigElement.AppendChild(newElement);

            newElement = ConfigDocument.CreateElement("password");
            newElement.InnerText = Password;
            ConfigElement.AppendChild(newElement);

            ConfigDocument.AppendChild(ConfigElement);

            ConfigDocument.Save(
                string.Format("{0}\\user_data.xml", Path));
        }

        public static string[] Load(string Path)
        {
            if (!File.Exists(Path))
                return null;

            string[] config = new string[2];

            XmlDocument ConfigDocument = new XmlDocument();
            ConfigDocument.Load(
                string.Format("{0}\\user_data.xml", Path));

            XmlElement RootElement = ConfigDocument.DocumentElement;
            config[0] = RootElement["username"].InnerText;
            config[1] = RootElement["password"].InnerText;

            return config;
        }
    }
}
