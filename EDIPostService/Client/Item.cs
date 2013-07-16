using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace EDIPostService.Client
{
    public class Item
    {
        #region properties
        public double weight { get; set; }

        public double height { get; set; }

        public double width { get; set; }

        public double length { get; set; }

        [XmlIgnore]
        public double cost { get; set; }

        [XmlIgnore]
        public double vat { get; set; }

        [XmlIgnore]
        public string itemNumber { get; set; }
        #endregion

        public Item(){
            this.cost = 0;
            this.vat = 0;
        }
        
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
