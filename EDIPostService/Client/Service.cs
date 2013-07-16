using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace EDIPostService.Client
{
    [XmlRoot("service")]
    public class Service
    {
        # region properties
        [XmlAttribute("id")]
        public int id { get; set; }

        [XmlElement("name")]
        public string name { get; set; }

        [XmlIgnore]
        public double cost { get; set; }

        [XmlIgnore]
        public double vat{ get; set; }
        # endregion
    }
}
