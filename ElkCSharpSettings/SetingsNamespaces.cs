using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ElkCSharpSettings
{
    public static class SetingsNamespaces
    {
        public static readonly string ns_settings = "http://dossytronics.com/ElkCSharp/Settings";

        public static XmlNamespaceManager GetNamspaceManager()
        {
            var ret = new XmlNamespaceManager(new NameTable());
            ret.AddNamespace("elk", ns_settings);
            return ret;
        }
    }
}
