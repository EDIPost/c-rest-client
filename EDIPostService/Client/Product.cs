using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDIPostService.Client
{
    public class Product
    {
        public int id { get; set; }

        public string name { get; set; }

        public string status { get; set; }

        public Transporter transporter { get; set; }

        public Services services { get; set; }
    }
}
