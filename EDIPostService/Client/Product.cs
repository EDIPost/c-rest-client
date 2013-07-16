using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace EDIPostService.Client
{
    [XmlRoot("product")]
    public class Product
    {
        [XmlAttribute("id")]
        public int id { get; set; }

        [XmlElement("name")]
        public string name { get; set; }

        [XmlElement("status")]
        public string status { get; set; }

        [XmlElement("transporter")]
        public Transporter transporter { get; set; }

        [XmlArray("services")]
        [XmlArrayItem("service")]
        public Services services { get; set; }
    }
}
