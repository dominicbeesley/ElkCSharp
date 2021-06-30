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
                ret.KeyMappings.Add(LoadKeyMap(ret, elkm, ns));
            }

            foreach (XmlElement elkmac in elSettings.SelectNodes("elk:machinedef", ns))
            {
                ret.MachineDefs.Add(LoadMachineDef(ret, elkmac, ns));
            }

            var km = elSettings.GetAttribute("keymap");
            var c = ret.KeyMappings.Where(k => k.Name == km).FirstOrDefault() ?? ret.KeyMappings.FirstOrDefault();
            if (c == null)
                throw new SettingsLoadException(elSettings, "There is no keymap with a name that matches the keymap attribute", null);
            c.SetCurrent();
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

        internal static MachineDef LoadMachineDef(Settings settings, XmlElement elkmac, XmlNamespaceManager ns)
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

        internal static KeyMap LoadKeyMap(Settings settings, XmlElement el, XmlNamespaceManager ns)
        {
            var ret = new KeyMap(settings) { Name = el.GetAttribute("name"), Description = el.GetAttribute("description") };
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
                        throw new SettingsLoadException(elkey, $"Bad key value \"{winkey}\"");
                    }

                    TriState shift;
                    var ss = elkey.GetAttribute("shift");
                    try
                    {
                        if (string.IsNullOrEmpty(ss))
                            shift = TriState.Any;
                        else
                            shift = Enum.Parse<TriState>(ss, true);
                    } catch (Exception ex)
                    {
                        throw new SettingsLoadException(elkey, $"Bad shift value \"{ss}\"", ex);
                    }

                    TriState ctrl;
                    var cs = elkey.GetAttribute("ctrl");
                    try
                    {
                        if (string.IsNullOrEmpty(cs))
                            ctrl = TriState.Any;
                        else
                            ctrl = Enum.Parse<TriState>(cs, true);
                    }
                    catch (Exception ex)
                    {
                        throw new SettingsLoadException(elkey, $"Bad ctrl value \"{cs}\"", ex);
                    }

                    List<KeyMatrix> kms = new List<KeyMatrix>();
                    foreach (XmlElement elkm in elkey.SelectNodes("elk:keymatrix", ns))
                    {

                        int col;
                        var colstr = elkm.GetAttribute("col");
                        try
                        {
                            col = Int32.Parse(colstr);
                            if (col < 0 || col >= 14)
                                throw new Exception();
                        }
                        catch (Exception)
                        {
                            throw new SettingsLoadException(elkm, $"Bad key col \"{colstr}\"");
                        }

                        int row;
                        var rowstr = elkm.GetAttribute("row");
                        try
                        {
                            row = Int32.Parse(rowstr);
                            if (row < 0 || row >= 4)
                                throw new Exception();
                        }
                        catch (Exception)
                        {
                            throw new SettingsLoadException(elkm, $"Bad key row \"{rowstr}\"");
                        }

                        bool on;
                        var ons = elkm.GetAttribute("force")?.ToLower();
                        if (String.IsNullOrEmpty(ons) || ons == "on")
                            on = true;
                        else if (ons == "off")
                            on = false;
                        else
                            throw new SettingsLoadException(elkm, $"Bad force \"{ons}\"");

                        kms.Add(new KeyMatrix() { Col = col, Row = row, On = on });
                    }


                    ret.Keys.Add(new KeyDef() { WindowsKey = (Key)k, Shift = shift, Ctrl = ctrl, KeyMatrices = kms.ToArray() });
                }
            }
            catch (Exception ex)
            {
                throw new SettingsLoadException(el, $"Error loading keymap {ret.Name}: {ex.Message}", ex);
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
