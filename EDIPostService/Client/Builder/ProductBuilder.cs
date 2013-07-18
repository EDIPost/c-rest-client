using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDIPostService.Client.Builder
{

    /// <summary>
    /// Builds a proper product object based on the values we have.
    /// </summary>
    public class ProductBuilder
    {
        #region properties
        /// <summary>
        /// The ID of the product
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Name of the product
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Textual description of the status.  AVAILABLE means it is avalable to be used with the 
        /// current Consignee - Consignor combination
        /// </summary>
        public string status { get; set; }


        /// <summary>
        /// The name of the transporter / Carrier
        /// </summary>
        public string transportername { get; set; }

        /// <summary>
        /// A list of services avaiable
        /// </summary>
        public Services services;
        #endregion


        /// <summary>
        /// Adds a service to the product
        /// </summary>
        /// <param name="s">The service object to be added</param>
        public void addService(Service s) {
            if (this.services == null)
            {
                this.services = new Services();
            }
            this.services.Add(s);
        }



        /// <summary>
        /// Builds the Product object
        /// </summary>
        /// <returns>The ready Product</returns>
        public Product build()
        {
            Product p = new Product();
            p.id = this.id;
            p.status = this.status;
            p.name = this.name;

            Transporter t = new Transporter();
            t.name = this.transportername;

            p.transporter = t;
            p.services = this.services;

            return p;
        }
    }
}
