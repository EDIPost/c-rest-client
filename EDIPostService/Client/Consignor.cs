using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EDIPostService.Client
{
    [XmlRoot("Consignor")]
    public class Consignor : Party
    {
        [XmlElement("id")]
        public int id { get; set; }
    }
}
