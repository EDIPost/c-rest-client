using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDIPostService.Client.Builder
{
    public class ProductBuilder
    {
        #region properties
        public int id { get; set; }

        public string name { get; set; }

        public string status { get; set; }

        public string transportername { get; set; }

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
