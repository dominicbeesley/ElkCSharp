using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ElkCSharpSettings
{
    public class SettingsLoadException : Exception
    {
        public XmlElement Xml { get; init; }

        public SettingsLoadException(XmlElement el, string message, Exception inner = null)
            : base($"Error loading settings: {message}", inner)
        {
            Xml = el;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Xml path: ");
            sb.AppendLine(XmlPath(Xml));
            sb.AppendLine();
            sb.AppendLine(InnerException.ToString());
            return sb.ToString();
        }

        protected string XmlPath(XmlElement el)
        {
            XmlElement parEl = el.ParentNode as XmlElement;
            string parents = (parEl == null) ? "" : XmlPath(parEl) + "/";
            string name = el.GetAttribute("name");
            int index = CountPrevSibs(el);

            return $"{parents}{(string.IsNullOrEmpty(el.Prefix) ? "" : el.Prefix + ":")}{el.LocalName}{(string.IsNullOrEmpty(name) ? "" : $"{{name={name}}}")}[{index}]";
        }

        protected int CountPrevSibs(XmlElement el)
        {
            int ret = 0;
            var p = el.PreviousSibling;
            while (p != null)
            {
                var pe = p as XmlElement;

                if (pe.LocalName == el.LocalName && pe.NamespaceURI == el.NamespaceURI)
                    ret++;

                p = p.PreviousSibling;
            }

            return ret;
        }
    }
}
