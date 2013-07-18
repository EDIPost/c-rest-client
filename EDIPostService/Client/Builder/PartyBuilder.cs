using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDIPostService.Client.Builder
{
    /// <summary>
    /// The base class of all parties.
    /// </summary>
    public class PartyBuilder
    {

        /// <summary>
        /// The id of the party
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// The company name
        /// <strong>mandatory</strong>
        /// </summary>
        public string companyName { get; set; }

        /// <summary>
        /// The customernumber
        /// </summary>
        public string customerNumber { get; set; }

        /// <summary>
        /// The street/delivery address
        /// </summary>
        public string streetAddress { get; set; }

        /// <summary>
        /// the zipcode for the street address
        /// </summary>
        public string streetZip { get; set; }

        /// <summary>
        /// The city name  
        /// </summary>
        public string streetCity { get; set; }

        /// <summary>
        /// The postal address
        /// </summary>
        public string postAddress { get; set; }

        /// <summary>
        /// The zipcode of the postal address
        /// </summary>
        public string postZip { get; set; }

        /// <summary>
        /// The city that the postal address belongs too
        /// </summary>
        public string postCity { get; set; }

        /// <summary>
        /// The country
        /// </summary>
        public string country { get; set; }

        /// <summary>
        /// Complete name of the contact person
        /// </summary>
        public string contactName { get; set; }

        /// <summary>
        /// The telephone number of the contact person
        /// </summary>
        public string contactPhone { get; set; }

        /// <summary>
        /// The cellphone number of the contact person.
        /// </summary>
        public string contactCellphone { get; set; }

        /// <summary>
        /// The email address of the contact person
        /// </summary>
        public string contactEmail { get; set; }

    }
}
