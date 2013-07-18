using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDIPostService.Client
{
    /// <summary>
    /// A contact object ( usually a person )
    /// </summary>
    public class Contact
    {

        /// <summary>
        /// The name of the contact
        /// </summary>
        public String name { get; set; }

        /// <summary>
        /// Telephonenumber of the contact
        /// </summary>
        public String telephone { get; set; }


        /// <summary>
        /// Cellphone number of the contact
        /// </summary>
        public String cellPhone { get; set; }


        /// <summary>
        /// The email address of the contact
        /// </summary>
        public String email { get; set; }

    }
}
