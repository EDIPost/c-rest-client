using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace EDIPostService.Client
{
    /// <summary>
    /// The consignment/shipment object
    /// </summary>
    [XmlRoot("consignment")]
    public class Consignment
    {
        /// <summary>
        /// The ID of the consignment
        /// </summary>
        [XmlAttribute("id")]
        public int id { get; set; }

        /// <summary>
        /// The consignor/sender of the consginment
        /// </summary>
        [XmlElement("consignor")]
        public Consignor consignor { get; set; }

        /// <summary>
        /// The consignee/recipient of the consignment
        /// </summary>
        [XmlElement("consignee")]
        public Consignee consignee { get; set; }

        /// <summary>
        /// The product/shipmenttype we want to use
        /// </summary>
        [XmlElement("product")]
        public Product product { get; set; }

        /// <summary>
        /// A list of items/connotes we want to ship
        /// </summary>
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

        /// <summary>
        /// The unique identifier of the consignment.
        /// </summary>
        [XmlIgnore]
        public string shipmentNumber { get; set; }

        /// <summary>
        /// Constructor class
        /// </summary>
        public Consignment()
        {
            this.contentReference = " ";
            this.transportInstructions = " ";
            this.internalReference = " ";
        }


        /// <summary>
        /// Fetches the total Postage for all items combined
        /// </summary>
        /// <param name="include_vat">To include VAT in the total or not.</param>
        /// <returns>a double containing the total postage</returns>
        public double TotalPostageCost(bool include_vat = false)
        {
            double total = 0;
            foreach (Item i in this.items)
            {
                total += i.cost;
                if (include_vat)
                {
                    total += i.vat;
                }
            }
            return total;
        }

        /// <summary>
        /// The total cost of all value added services combined
        /// </summary>
        /// <param name="include_vat">To include VAT in the total or not.</param>
        /// <returns>The total</returns>
        public double TotalServiceCost(bool include_vat = false)
        {
            double total = 0;
            foreach (Service s in this.product.services)
            {
                total += s.cost;
                if (include_vat)
                {
                    total += s.vat;
                }
            }
            return total;
        }

    }


    
}
