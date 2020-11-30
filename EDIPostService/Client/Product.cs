using System.Xml.Serialization;

namespace EDIPostService.Client
{
    /// <summary>
    /// The product class.
    /// </summary>
    [XmlRoot("product")]
    public class Product
    {
        /// <summary>
        /// The id of the product
        /// </summary>
        [XmlAttribute("id")]
        public int id { get; set; }


        /// <summary>
        /// The name of the product
        /// </summary>
        [XmlElement("name")]
        public string name { get; set; }

        /// <summary>
        /// The status of the product
        /// <remarks>AVAILABLE is generally what you should look for.</remarks>
        /// </summary>
        [XmlElement("status")]
        public string status { get; set; }

        /// <summary>
        /// The transporter object. Information about the transporter the product belongs too.
        /// </summary>
        [XmlElement("transporter")]
        public Transporter transporter { get; set; }

        /// <summary>
        /// A list of value adding services available with this product
        /// </summary>
        [XmlArray("services")]
        [XmlArrayItem("service")]
        public Services services { get; set; }

        /// <summary>
        /// Custom ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "ID: " + id + ", Name: " + name + ", Transporter: " + transporter.name + ", Status: " + status;
        }
    }
}
