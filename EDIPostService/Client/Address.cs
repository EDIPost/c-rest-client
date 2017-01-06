using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDIPostService.Client
{
    /// <summary>
    /// Class to hold information about an address
    /// </summary>
    public class Address
    {
        /// <summary>
        /// The addressline
        /// </summary>
        public String address { get; set; }

        /// <summary>
        /// Address line 2
        /// </summary>
        public String address2 { get; set; }

        /// <summary>
        /// The zipcode or postal code
        /// </summary>
        public String zipCode { get; set; }

        /// <summary>
        /// The city the address belongs to
        /// </summary>
        public String city { get; set; }
    }
}
