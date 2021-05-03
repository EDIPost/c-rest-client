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
        /// The URL to the direct transporter track and trace.
        /// </summary>
        [XmlElement("trackAndTraceUrl")]
        public string trackAndTraceUrl { get; set; }

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
        /// Fetches the total Postage for consignment
        /// </summary>
        /// <param name="includeVat">To include VAT in the total or not.</param>
        /// <returns>a double containing the total postage</returns>
        public double TotalCost(bool includeVat = false)
        {
            return TotalPostageCost(includeVat) + TotalServiceCost(includeVat);
        }

        /// <summary>
        /// Fetches the total Postage for all items combined
        /// </summary>
        /// <param name="includeVat">To include VAT in the total or not.</param>
        /// <returns>a double containing the total postage</returns>
        public double TotalPostageCost(bool includeVat = false)
        {
            double total = 0;
            foreach (Item i in this.items)
            {
                total += i.cost;
                if (includeVat)
                {
                    total += i.vat;
                }
            }
            return total;
        }

        /// <summary>
        /// The total cost of all value added services combined
        /// </summary>
        /// <param name="includeVat">To include VAT in the total or not.</param>
        /// <returns>The total</returns>
        public double TotalServiceCost(bool includeVat = false)
        {
            double total = 0;
            if (this.product != null && this.product.services != null)
            {
                foreach (Service s in this.product.services)
                {
                    total += s.cost;
                    if (includeVat)
                    {
                        total += s.vat;
                    }
                }
            }
            return total;
        }

    }
}
