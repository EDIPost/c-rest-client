using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDIPostService.Client;

namespace EDIPostService
{
    interface iEDIPostService
    {

        /// <summary>
        /// Fetches a default consignor
        /// </summary>
        /// <returns>Consignor</returns>
        Consignor getDefaultConsignor();


        /// <summary>
        /// Creates a consignee party ( recipient )
        /// </summary>
        /// <param name="consignee"></param>
        /// <returns>Consignee</returns>
        Consignee createConsignee(Consignee consignee);

    }
}
