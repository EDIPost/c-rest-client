using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDIPostService.Client
{
    public class Item
    {
        #region properties
        public double weight { get; set; }

        public double height { get; set; }

        public double width { get; set; }

        public double length { get; set; }
        #endregion

        public Item( double weight = 0, double height = 0, double width = 0, double length = 0 ){
            this.weight = weight;
            this.height = height;
            this.width = width;
            this.length = length;
        }
    }
}
