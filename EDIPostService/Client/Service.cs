using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace EDIPostService.Client
{
    /// <summary>
    /// Additional services to the product.
    /// </summary>
    [XmlRoot("service")]
    public class Service
    {
        # region properties
        /// <summary>
        /// The id of the service
        /// </summary>
        [XmlAttribute("id")]
        public int id { get; set; }

        /// <summary>
        /// The name of the service
        /// </summary>
        [XmlElement("name")]
        public string name { get; set; }

        /// <summary>
        /// What the transporter/carrier will charge for this service. 
        /// <remarks>excluding VAT</remarks>
        /// </summary>
        [XmlIgnore]
        public double cost { get; set; }

        /// <summary>
        /// The VAT of the cost
        /// </summary>
        [XmlIgnore]
        public double vat{ get; set; }
        # endregion
    }
}
