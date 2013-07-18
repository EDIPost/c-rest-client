using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EDIPostService.Client
{

    /// <summary>
    /// The base class of any Party, i.e Consignor, Consignee, Payer ...
    /// </summary>
    public class Party
    {
        /// <summary>
        /// The id of the party
        /// </summary>
        [XmlAttribute("id")]
        public int id { get; set; }

        /// <summary>
        /// The companyname of the party
        /// <remarks>Mandatory</remarks>
        /// </summary>
        [XmlElement("companyName")]
        public String companyName { get; set; }

        /// <summary>
        /// The customernumber 
        /// </summary>
        [XmlElement("customerNumber")]
        public String customerNumber { get; set; }


        /// <summary>
        /// The country the party originates in
        /// </summary>
        [XmlElement("country")]
        public String country { get; set; }

        /// <summary>
        /// The postal address of the party
        /// </summary>
        [XmlElement("postAddress")]
        public Address postAddress { get; set; }

        /// <summary>
        /// The street/delivery address of the party
        /// </summary>
        [XmlElement("streetAddress")]
        public Address streetAddress { get; set; }

        /// <summary>
        /// The contact affiliated to the party, typical a person
        /// </summary>
        [XmlElement("contact")]
        public Contact contact { get; set; }
    }
}
