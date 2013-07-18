using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace EDIPostService.Client
{
    /// <summary>
    /// An item of the shipment, also know as a connote.
    /// </summary>
    public class Item
    {
        #region properties
        /// <summary>
        /// The weight of the item
        /// </summary>
        public double weight { get; set; }

        /// <summary>
        /// The height of the item
        /// </summary>
        public double height { get; set; }

        /// <summary>
        /// The width of the item
        /// </summary>
        public double width { get; set; }

        /// <summary>
        /// The length of the item
        /// </summary>
        public double length { get; set; }

        /// <summary>
        /// The cost of the item, aka the postage excluding VAT. and services
        /// </summary>
        [XmlIgnore]
        public double cost { get; set; }

        /// <summary>
        /// The VAT value if aplicable and based on country of origin
        /// </summary>
        [XmlIgnore]
        public double vat { get; set; }

        /// <summary>
        /// The unique identifier of the item
        /// </summary>
        [XmlIgnore]
        public string itemNumber { get; set; }
        #endregion
        

        /// <summary>
        /// Constructor with no parameters. 
        /// </summary>
        public Item(){
            this.cost = 0;
            this.vat = 0;
        }
        

        /// <summary>
        /// Constructor with 4 parameter to easily create an item
        /// </summary>
        /// <param name="weight">The weight</param>
        /// <param name="height">The height</param>
        /// <param name="width">The width</param>
        /// <param name="length">The length</param>
        public Item( double weight = 0, double height = 0, double width = 0, double length = 0 ) : this() {
            this.weight = weight;
            this.height = height;
            this.width = width;
            this.length = length;

            this.cost = 0;
            this.vat = 0;
        }
    }
}
