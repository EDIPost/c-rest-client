using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EDIPostService.Client
{
    /// <summary>
    /// The consignee class
    /// </summary>
    [XmlRoot("consignee")]
    public class Consignee : Party
    {
       /* [XmlAttribute("id")]
        public int id { get; set; } */
    }
}
