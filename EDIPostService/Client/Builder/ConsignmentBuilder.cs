using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDIPostService.Client.Builder
{
    class ConsignmentBuilder
    {
        
        # region properties
        public int id { get; set; }

        public int consignorID { get; set; }

        public int consigneeID { get; set; }

        public int productID { get; set; }

        public List<Item> items { get; }

        public List<Service> services { get; }

        public string contentReference { get; set; }

        public string transportInstructions { get; set; }

        public string internalReference { get; set; }
        # endregion



    }
}
