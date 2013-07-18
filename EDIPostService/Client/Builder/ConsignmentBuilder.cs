using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDIPostService.Client;

namespace EDIPostService.Client.Builder
{
    /// <summary>
    /// Builder class to create a consignment with the bare minimum information.
    /// </summary>
    public class ConsignmentBuilder
    {
        
        # region properties
        /// <summary>
        /// the id of the consignment
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// The id of the consignor
        /// </summary>
        public int consignorID { get; set; }

        /// <summary>
        /// The id of the consignee
        /// </summary>
        public int consigneeID { get; set; }

        /// <summary>
        /// The id of the product
        /// </summary>
        public int productID { get; set; }

        /// <summary>
        /// A list of items 
        /// </summary>
        public Items items { get; set; }

        /// <summary>
        /// a list of services
        /// </summary>
        public Services services { get; set; }

        /// <summary>
        /// Information to the consignee/recipient
        /// <remarks>also available in the preadvice file</remarks>
        /// </summary>
        public string contentReference { get; set; }

        /// <summary>
        /// Information to the Transporter/Carrier
        /// <remarks>Also avaialable in the preadvice file</remarks>
        /// </summary>
        public string transportInstructions { get; set; }

        /// <summary>
        /// An internal reference. Only for internal use, but is searchable in the archive
        /// </summary>
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

            c.contentReference = this.contentReference ?? "&nbsp;";
            c.transportInstructions = this.transportInstructions ?? "&nbsp;";
            c.internalReference = this.internalReference ?? "&nbsp;";

            return c;
        }

    }
}
