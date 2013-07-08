using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDIPostService.Client.Builder
{
    public class ConsignorBuilder : PartyBuilder
    {

        public Consignor build()
        {
            Consignor c = new Consignor();
            c.id = id;
            c.companyName = companyName;
            c.customerNumber = customerNumber;

            Address pa = new Address();
            pa.address = postAddress;
            pa.zipCode = postZip;
            pa.city = postCity;

            Address sa = new Address();
            sa.address = streetAddress;
            sa.zipCode = streetZip;
            sa.city = streetCity;

            Contact co = new Contact();
            co.name = contactName;
            co.cellPhone = contactCellphone;
            co.telephone = contactPhone;
            co.email = contactEmail;

            c.postAddress = pa;
            c.streetAddress = sa;
            c.contact = co;

            return c;
        }
    }
}
