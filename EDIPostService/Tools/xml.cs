using EDIPostService.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace EDIPostService.Tools
{
    public class xml
    {

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

            xml.LoadXml(Encoding.UTF8.GetString(m.ToArray()));
            
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

        
    }
}
