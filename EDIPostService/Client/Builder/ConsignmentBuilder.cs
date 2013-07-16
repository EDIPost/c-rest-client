using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDIPostService.Client;

namespace EDIPostService.Client.Builder
{
    public class ConsignmentBuilder
    {
        
        # region properties
        public int id { get; set; }

        public int consignorID { get; set; }

        public int consigneeID { get; set; }

        public int productID { get; set; }

        public Items items { get; set; }

        public Services services { get; set; }

        public string contentReference { get; set; }

        public string transportInstructions { get; set; }

        public string internalReference { get; set; }
        # endregion


        /// <summary>
        /// Adds a item to the items container
        /// </summary>
        /// <param name="i">the item to be added.</param>
        public void addItem(Item i)
        {
            if (this.items == null)
            {
                this.items = new Items();
            }
            this.items.Add(i);
        }


        /// <summary>
        /// Adds a service object to the services container
        /// </summary>
        /// <param name="s">The service to be added</param>
        public void addService(Service s)
        {
            if (this.services == null)
            {
                this.services = new Services();
            }
            this.services.Add(s);
        }


        /// <summary>
        /// Builds the consignment object
        /// </summary>
        /// <returns>The consignment filled with values</returns>
        public Consignment build()
        {
            Consignment c = new Consignment();

            // Consignee
            Consignee consignee = new Consignee();
            consignee.id = this.consigneeID;
            c.consignee = consignee;

            // Consignor
            Consignor consignor = new Consignor();
            consignor.id = this.consignorID;
            c.consignor = consignor;

            // Product
            Product product = new Product();
            product.id = this.productID;

            // Add Services to Product
            product.services = this.services ?? new Services();
            c.product = product;

            // Items
            c.items = this.items ?? new Items();

            c.contentReference = this.contentReference ?? " ";
            c.transportInstructions = this.transportInstructions ?? " ";
            c.internalReference = this.internalReference ?? " ";

            return c;
        }

    }
}
