using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Input;

namespace ElkCSharpSettings
{
    public static class SettingsFactory
    {
        public static Settings LoadSettings(XmlElement elSettings, XmlNamespaceManager ns)
        {
            var ret = new Settings();

            foreach (XmlElement elkm in elSettings.SelectNodes("elk:keymap", ns))
            {
                ret.KeyMappings.Add(LoadKeyMap(elkm, ns));
            }

            return ret;
        }

        public static Settings LoadSettings(string filename)
        {
            var ns = SetingsNamespaces.GetNamspaceManager();
            var doc = new XmlDocument();
            doc.Load(filename);

            if (doc?.DocumentElement?.NamespaceURI != SetingsNamespaces.ns_settings)
                throw new ArgumentException("Missing elk:settings element");

            return LoadSettings(doc.DocumentElement, SetingsNamespaces.GetNamspaceManager());
        }

        public static XmlElement SaveSettings(Settings settings, XmlDocument doc)
        {
            var ret = doc.CreateElement("settings", SetingsNamespaces.ns_settings);
            foreach (var km in settings.KeyMappings)
            {
                ret.AppendChild(SaveKeyMapping(km, doc));
            }
            return ret;
        }

        internal static XmlNode SaveKeyMapping(KeyMap km, XmlDocument doc)
        {
            var ret = doc.CreateElement("elk", "keymap", SetingsNamespaces.ns_settings);
            ret.SetAttribute("name", km.Name);
            foreach(var k in km.Keys) {
                var elk = doc.CreateElement("elk", "keydef", SetingsNamespaces.ns_settings);
                elk.SetAttribute("winkey", k.Key.ToString());
                elk.SetAttribute("row", k.Row.ToString());
                elk.SetAttribute("col", k.Col.ToString());
            }
            return ret;
        }

        public static void SaveSettings(Settings settings, string filename)
        {
            var ns = SetingsNamespaces.GetNamspaceManager();
            XmlDocument doc = new XmlDocument();
            doc.AppendChild(SaveSettings(settings, doc));
            doc.Save(filename);
        }

        internal static KeyMap LoadKeyMap(XmlElement el, XmlNamespaceManager ns)
        {
            var ret = new KeyMap() { Name = el.GetAttribute("name") };
            try
            {
                foreach (XmlElement elkey in el.SelectNodes("elk:keydef", ns))
                {
                    Key? k;
                    var winkey = elkey.GetAttribute("winkey");
                    try
                    {
                        k = (Key)Enum.Parse(typeof(Key), winkey);
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException($"Bad key value \"{winkey}\"");
                    }

                    int col;
                    var colstr = elkey.GetAttribute("col");
                    try
                    {
                        col = Int32.Parse(colstr);
                        if (col < 0 || col >= 14)
                            throw new Exception();
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException($"Bad key col \"{colstr}\"");
                    }

                    int row;
                    var rowstr = elkey.GetAttribute("row");
                    try
                    {
                        row = Int32.Parse(rowstr);
                        if (row < 0 || row >= 4)
                            throw new Exception();
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException($"Bad key row \"{rowstr}\"");
                    }


                    ret.Keys.Add(new KeyMap.KeyDef() { Key = (Key)k, Col = (int)col, Row = (int)row });
                }
            } catch (Exception ex)
            {
                throw new ArgumentException($"Error loading keymap {ret.Name}: {ex.Message}", ex);
            }
            return ret;
        }

    }
}
