using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace EDIPostService.Client
{
    [XmlRoot("consignment")]
    public class Consignment
    {
        [XmlAttribute("id")]
        public int id { get; set; }

        [XmlElement("consignor")]
        public Consignor consignor { get; set; }

        [XmlElement("consignee")]
        public Consignee consignee { get; set; }

        [XmlElement("product")]
        public Product product { get; set; }

        [XmlArray("items")]
        [XmlArrayItem("item")]
        public Items items { get; set; }

        /// <summary>
        /// Reference for the recipient
        /// Minimum length 1, maximum 70
        /// </summary>
        [XmlElement("contentReference")]
        public string contentReference { get; set; }

        /// <summary>
        /// Special notification for the carrier. 
        /// Minimum length 1, maximum 140
        /// </summary>
        [XmlElement("transportInstructions")]
        public string transportInstructions { get; set; }

        /// <summary>
        /// Internal reference.
        /// Minimum length 1, maximum 70
        /// </summary>
        [XmlElement("internalReference")]
        public string internalReference { get; set; }

        [XmlIgnore]
        public string shipmentNumber { get; set; }


        public Consignment()
        {
            this.contentReference = " ";
            this.transportInstructions = " ";
            this.internalReference = " ";
        }

    }


    
}
