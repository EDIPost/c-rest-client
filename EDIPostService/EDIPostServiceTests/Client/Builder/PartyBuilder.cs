﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDIPostService.Client;
using EDIPostService.Client.Builder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EDIPostServiceTests.Client.Builder
{
    [TestClass]
    public class PartyBuilderTest
    {
        [TestMethod]
        public void PartyBuilderTest_test_getsets()
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

            Assert.AreEqual(expectedValue, checkValue);
        }

        [TestMethod]
        public void ConsignorBuilderTest_return_consignor()
        {
            ConsignorBuilder b = ConsignorBuilder(); 
            Consignor c = b.build();

            Assert.IsInstanceOfType( c, typeof(Consignor) );
        }

        [TestMethod]
        public void ConsignorBuilderTest_correct_id()
        {
            ConsignorBuilder b = ConsignorBuilder();
            Consignor c = b.build();

            Assert.AreEqual(1, c.id);
        }

        [TestMethod]
        public void ConsignorBuilderTest_correct_parent_type()
        {
            ConsignorBuilder b = ConsignorBuilder();
            Consignor c = b.build();

            //Assert.IsAssignableFrom<Party>(c);
        }



        private ConsignorBuilder ConsignorBuilder()
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

            return b;
        }
    }
}
