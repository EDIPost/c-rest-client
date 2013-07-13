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

        Consignor getDefaultConsignor();

        Consignee createConsignee(Consignee consignee);

    }
}
