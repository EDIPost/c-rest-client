using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDIPostService.Client
{
    public class Consignment
    {
        public Consignor consignor { get; set; }

        public Consignee consignee { get; set; }

        public Product product { get; set; }

        public Items items { get; set; }

        public string contentReference { get; set; }

        public string transportInstruction { get; set; }

        public string internalReference { get; set; }
    }


    
}
