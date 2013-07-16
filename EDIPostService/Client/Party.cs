using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EDIPostService.Client
{
    public class Party
    {
        [XmlAttribute("id")]
        public int id { get; set; }

        [XmlElement("companyName")]
        public String companyName { get; set; }

        [XmlElement("customerNumber")]
        public String customerNumber { get; set; }

        [XmlElement("country")]
        public String country { get; set; }

        [XmlElement("postAddress")]
        public Address postAddress { get; set; }

        [XmlElement("streetAddress")]
        public Address streetAddress { get; set; }

        [XmlElement("contact")]
        public Contact contact { get; set; }
    }
}
