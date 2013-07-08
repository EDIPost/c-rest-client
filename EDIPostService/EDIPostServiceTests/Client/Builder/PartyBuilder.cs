using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using EDIPostService.Client;
using EDIPostService.Client.Builder;

namespace EDIPostServiceTests.Client.Builder
{
    public class PartyBuilderTest
    {
        [Fact]
        public void PartyBuilderTest1()
        {
            PartyBuilder pb = new PartyBuilder();
            pb.id = 1;
            pb.companyName = "CompanyName";
            pb.customerNumber = "CustomerNumber";
            pb.postAddress = "PostAddress";
            pb.postCity = "PostCity";
            pb.postZip = "PostZip";
            pb.streetAddress = "StreetAddress";
            pb.streetCity = "StreetCity";
            pb.streetZip = "StreetZip";
            pb.country = "Country";
            pb.contactName = "ContactName";
            pb.contactPhone = "ContactPhone";
            pb.contactEmail = "ContactEmail";
            pb.contactCellphone = "ContactCellphone";

            String checkValue = pb.companyName + pb.customerNumber + pb.postAddress + pb.postCity + pb.postZip + pb.streetAddress +
                                pb.streetCity + pb.streetZip + pb.country + pb.contactName + pb.contactPhone + pb.contactEmail + pb.contactCellphone;
            String expectedValue = "CompanyNameCustomerNumberPostAddressPostCityPostZipStreetAddressStreetCityStreetZipCountryContactNameContactPhoneContactEmailContactCellphone";

            Assert.Equal(expectedValue, checkValue);
        }

        [Fact]
        public void ConsignorBuilderTest1()
        {
            ConsignorBuilder b = new ConsignorBuilder();
            b.id = 1;
            b.companyName = "CompanyName";
            b.customerNumber = "CustomerNumber";
            b.postAddress = "PostAddress";
            b.postCity = "PostCity";
            b.postZip = "PostZip";
            b.streetAddress = "StreetAddress";
            b.streetCity = "StreetCity";
            b.streetZip = "StreetZip";
            b.country = "Country";
            b.contactName = "ContactName";
            b.contactPhone = "ContactPhone";
            b.contactEmail = "ContactEmail";
            b.contactCellphone = "ContactCellphone";

            Consignor c = b.build();

            Assert.False(true);
            
        }
    }
}
