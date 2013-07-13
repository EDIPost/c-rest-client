using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Xunit;
using EPService = EDIPostService;
using EPClient = EDIPostService.Client;
using EPBuilder = EDIPostService.Client.Builder;
using System.Net;


namespace EDIPostServiceTests
{
    public class EDIPostService
    {
        [Fact]
        public void EDIPostServiceTest_OptionalConstructorParam()
        {
            EPService.EDIPostService ep = new EPService.EDIPostService("213131");

            Assert.False(false);
        }

        [Fact]
        public void GetDefaultConsignorTest_correct_type()
        {
            EPService.EDIPostService ep = new EPService.EDIPostService("cc1b9a01af40d50ea6776d449f8afe9707c77750", "http://apitest.edipost.no/");
            EPClient.Consignor c = ep.getDefaultConsignor();

            Assert.IsType<EPClient.Consignor>(c);
        }

        [Fact]
        public void GetDefaultConsignorTest_correct_data()
        {
            EPService.EDIPostService ep = new EPService.EDIPostService("cc1b9a01af40d50ea6776d449f8afe9707c77750", "http://apitest.edipost.no/");
            EPClient.Consignor c = ep.getDefaultConsignor();

            Assert.Equal(1305799, c.id);
        }

        [Fact]
        public void createConsigneeTest_domestic_request()
        {
            EPService.EDIPostService ep = new EPService.EDIPostService("cc1b9a01af40d50ea6776d449f8afe9707c77750", "http://apitest.edipost.no/");

            // Create the consignee object
            EPClient.Consignee c = _getTestConsignee();
            EPClient.Consignee rc = ep.createConsignee(c);

            Assert.IsAssignableFrom<EPClient.Consignee>(rc);
        }



        private EPClient.Consignee _getTestConsignee()
        {
            EPBuilder.ConsigneeBuilder cb = new EPBuilder.ConsigneeBuilder();
            cb.companyName = "Test firma";
            cb.customerNumber = "007";
            cb.streetAddress = "Street address";
            cb.streetCity = "StreetCity";
            cb.streetZip = "StreetZip";
            cb.postAddress = "Postal address";
            cb.postCity = "Post City";
            cb.postZip = "PostZip";
            cb.country = "NO";
            cb.contactName = "Contact person";
            cb.contactPhone = "611 59010";
            cb.contactCellphone = "93 44 93 44";
            cb.contactEmail = "teknisk@edipost.no";

            EPClient.Consignee c = cb.build();
            return c;
        }

    }
}

