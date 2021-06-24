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

            foreach (XmlElement elkmac in elSettings.SelectNodes("elk:machinedef", ns))
            {
                ret.MachineDefs.Add(LoadMachineDef(elkmac, ns));
            }

            return ret;
        }

        public static Settings LoadSettings(string filename)
        {
            var ns = SetingsNamespaces.GetNamspaceManager();
            var doc = new XmlDocument();
            doc.Load(filename);

            if (doc?.DocumentElement?.NamespaceURI != SetingsNamespaces.ns_settings)
                throw new SettingsLoadException(doc.DocumentElement, "Missing elk:settings element", null);

            return LoadSettings(doc.DocumentElement, SetingsNamespaces.GetNamspaceManager());
        }

        internal static MachineDef LoadMachineDef(XmlElement elkmac, XmlNamespaceManager ns)
        {
            var ret = new MachineDef() { Name = elkmac.GetAttribute("name") };
            try
            {
                foreach (XmlElement elrd in elkmac.SelectNodes("elk:romset/elk:romdef", ns))
                {
                    try
                    {
                        int num = int.Parse(elrd.GetAttribute("number"));
                        if (num < 0 || num > 16)
                            throw new ArgumentException($"Rom number {num} out of range 0<=x<=16");
                        if (ret.RomDefs.Where(m => m.Number == num).Any())
                            throw new ArgumentException($"Rom number {num} already defined");

                        bool we = Xml2Bool(elrd.GetAttribute("writeenable"));
                        string load = elrd.GetAttribute("load");

                        ret.RomDefs.Add(new RomDef()
                        {
                            Number = num,
                            Load = load,
                            WriteEnable = we
                        });
                    }
                    catch (Exception ex)
                    {
                        throw new SettingsLoadException(elrd, $"Error loading rom def in machine={ret.Name}", ex);
                    }

                }
            }
            catch (SettingsLoadException)
            {
                throw;
            }
            catch (Exception ex)
            {
                new SettingsLoadException(elkmac, $"Error loading machinedef {ret.Name}: {ex.Message}", ex);
            }
            return ret;
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


                    ret.Keys.Add(new KeyDef() { Key = (Key)k, Col = (int)col, Row = (int)row });
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error loading keymap {ret.Name}: {ex.Message}", ex);
            }
            return ret;
        }

        private static bool Xml2Bool(string xmlstr)
        {
            if (string.IsNullOrEmpty(xmlstr) || xmlstr == "0" || xmlstr.ToLower() == "false")
                return false;
            else if (xmlstr == "1" || xmlstr.ToLower() == "true")
                return true;
            else
                throw new ArgumentException($"Bad boolean value \"{xmlstr}\"");
        }
    }
}
