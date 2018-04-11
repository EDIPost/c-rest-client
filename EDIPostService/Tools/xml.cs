using EDIPostService.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Net;
using System.Globalization;

namespace EDIPostService.Tools
{

    /// <summary>
    /// Tools to use for interal use
    /// </summary>
    public class xml
    {



        /// <summary>
        /// Serialize an object into an XmlDocument
        /// </summary>
        /// <typeparam name="T">The object type to be serialized</typeparam>
        /// <param name="obj">The object to serialize</param>
        /// <param name="remove_root_id">Remove the id property of the root element.</param>
        /// <returns>XmlDocument representation of the object obj</returns>
        public static XmlDocument format<T>(T obj, bool remove_root_id = false){

            XmlDocument xml = new XmlDocument();
            MemoryStream m = new MemoryStream();
            XmlSerializerNamespaces xns = new XmlSerializerNamespaces();
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());

            xns.Add(string.Empty, string.Empty);
            
            using (TextWriter writer = new StreamWriter(m))
            {
                serializer.Serialize(writer, obj, xns);
                writer.Close();
            }
            
            string utf8_string = Encoding.UTF8.GetString(m.ToArray());
            xml.LoadXml(utf8_string);
            
            if (remove_root_id)
            {
                var root = xml.SelectSingleNode("//*[contains(@id,'0')]");
                if (root != null)
                {
                    root.Attributes.Remove(root.Attributes["id"]);
                }
            }
            return xml;
        }




        /// <summary>
        /// Fetches the value of a node based on xPath or defaults
        /// </summary>
        /// <param name="n">The node to get the value from</param>
        /// <param name="xpath">XPath of the node</param>
        /// <param name="numeric">If numeric set retvalue to 0 instead of empty string</param>
        /// <returns>string</returns>
        public static string nodeValue(XmlNode n, string xpath, bool numeric = false )
        {
            string retvalue = "";
            int i = 0;
            decimal y;

            if ( n.SelectSingleNode(xpath) != null ){
                if ( n.SelectSingleNode(xpath).Value != null ){
                    retvalue = n.SelectSingleNode(xpath).Value;
                }else{
                    retvalue = n.SelectSingleNode(xpath).InnerText;
                }
            }

            if (numeric && !int.TryParse(retvalue, out i) && (!decimal.TryParse(retvalue.Replace(",", "").Replace(".", ""), NumberStyles.Number, CultureInfo.InvariantCulture, out y)))
            {
                retvalue = "0";
            }

            
            return retvalue;
        }



        public static string prettyPrint(XmlDocument doc) {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace
            };
            using (XmlWriter writer = XmlWriter.Create(sb, settings)) {
                doc.Save(writer);
            }
            return sb.ToString();
        }


    }
}
