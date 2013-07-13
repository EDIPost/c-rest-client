using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDIPostService.Client.Builder
{
    public class ConsigneeBuilder : PartyBuilder
    {

        public Consignee build()
        {
            Consignee c = new Consignee();
            c.id = id;
            c.companyName = companyName;
            c.customerNumber = customerNumber;
            c.country = country;

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
