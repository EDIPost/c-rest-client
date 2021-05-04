using System;
using System.Collections.Generic;
using System.Text;
using EPService = EDIPostService;
using EPBuilder = EDIPostService.Client.Builder;
using EDIPostService.Client;
using EDIPostService.ServiceConnection.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace EDIPostServiceTests
{
    [TestClass]
    public class EdiPostService
    {
        public const String ApiUrl = "http://api.dev.edipost.no/";
        public const String ApiKey = "07add61e089e3e8d3a1a7e34e71f462eee2ef8f5";
        public const int DefaultConsignorId = 3311;
        public const int ConsigneeId = 3270125;
        public const int ConsignmentId = 3331708;
        public const int ConsignmentZplId = 3334708;


        [TestMethod]
        public void EDIPostServiceTest_OptionalConstructorParam()
        {
            EPService.EDIPostService ep = new EPService.EDIPostService("213131");
            Assert.IsNotNull(ep);
        }

        [TestMethod]
        public void GetDefaultConsignorTest_correct_type()
        {
            EPService.EDIPostService ep = new EPService.EDIPostService(ApiKey, ApiUrl);
            Consignor c = ep.getDefaultConsignor();

            Assert.IsInstanceOfType(c, typeof(Consignor));
        }


        [TestMethod]
        public void GetDefaultConsignorTest_correct_data()
        {
            EPService.EDIPostService ep = new EPService.EDIPostService(ApiKey, ApiUrl);
            Consignor c = ep.getDefaultConsignor();

            Assert.AreEqual(DefaultConsignorId, c.id);
        }


        [TestMethod]
        public void createConsigneeTest_domestic_request()
        {
            EPService.EDIPostService ep = new EPService.EDIPostService(ApiKey, ApiUrl);

            // Create the consignee object
            Consignee c = _getTestConsignee();
            Consignee rc = ep.createConsignee(c);

            Assert.IsTrue(rc != null);
        }


        [TestMethod]
        public void createConsigneeTest_specialChars()
        {
            EPService.EDIPostService ep = new EPService.EDIPostService(ApiKey, ApiUrl);

            // Create the consignee object
            Consignee c = _getTestConsignee_specialChars();
            Consignee rc = ep.createConsignee(c);

            Assert.IsTrue(rc != null);
        }


        [TestMethod]
        public void RemoveConsigneeTest() {
            EPService.EDIPostService ep = new EPService.EDIPostService(ApiKey, ApiUrl);

            // Create the consignee object
            Consignee c = _getTestConsignee();

            Consignee rc = ep.createConsignee(c);
            ep.removeConsignee(rc.id);
        }


        /**
         * Test if we can make 3 create, get and delete operations without causing a timeout
         */
        [TestMethod]
        public void TimeoutTest() {
            EPService.EDIPostService ep = new EPService.EDIPostService(ApiKey, ApiUrl);

            // Create the consignee object
            Consignee c = _getTestConsignee();

            Consignee consignee1 = ep.createConsignee(c);
            Consignee consigneeFromDb1 = ep.getConsignee(consignee1.id);
            Assert.AreEqual(consignee1.id, consigneeFromDb1.id);
            ep.removeConsignee(consignee1.id);

            Consignee consignee2 = ep.createConsignee(c);
            Consignee consigneeFromDb2 = ep.getConsignee(consignee2.id);
            Assert.AreEqual(consignee2.id, consigneeFromDb2.id);
            ep.removeConsignee(consignee2.id);

            Consignee consignee3 = ep.createConsignee(c);
            Consignee consigneeFromDb3 = ep.getConsignee(consignee3.id);
            Assert.AreEqual(consignee3.id, consigneeFromDb3.id);
            ep.removeConsignee(consignee3.id);
        }


        [TestMethod]
        [ExpectedException(typeof(HttpException), "It should not be possible to remove a non-exsiting consignee ID")]
        public void RemoveConsigneeNonExistingConsingneeTest() {
            EPService.EDIPostService ep = new EPService.EDIPostService(ApiKey, ApiUrl);

            ep.removeConsignee(987654321);  // Non-existing ID
        }


        [TestMethod]
        public void searchConsigneeTest_type()
        {
            EPService.EDIPostService ep = new EPService.EDIPostService(ApiKey, ApiUrl);
            List<Consignee> consignees = ep.searchConsignee("Folco");

            Assert.IsTrue(consignees.Count > 0, "Less than 1 consignees returned");

            Assert.IsTrue(consignees[0] != null, "Result set has wrong type");
        }


        [TestMethod]
        public void findProductTest_domestic()
        {
            EPService.EDIPostService ep = new EPService.EDIPostService(ApiKey, ApiUrl);

            Consignee testConsignee = _getTestConsignee();
            Consignee consignee = ep.createConsignee(testConsignee);

            Consignor consignor = ep.getDefaultConsignor();

            EPBuilder.ConsignmentBuilder cb = new EPBuilder.ConsignmentBuilder
            {
                consigneeID = consignee.id,
                consignorID = consignor.id
            };

            cb.addItem(new Item(10, 20, 30, 40));
            cb.addItem(new Item(2.5, 25, 35, 45));
            
            
            Consignment c = cb.build();
            
            List<Product> products = ep.findProducts(c);

            Assert.IsTrue(products.Count > 0);
        }


        [TestMethod]
        public void FindAllProductsTest()
        {
            EPService.EDIPostService ep = new EPService.EDIPostService(ApiKey, ApiUrl);

            List<Product> products = ep.findAllProducts();
            products.ForEach(Console.WriteLine);

            Assert.IsTrue(products.Count > 0);
        }


        [TestMethod]
        public void createConsignmentTest_returntype()
        {
            EPService.EDIPostService ep = new EPService.EDIPostService(ApiKey, ApiUrl);
            Consignor consignor = ep.getDefaultConsignor();

            Consignee testConsignee = _getTestConsignee();
            Consignee consignee = ep.createConsignee(testConsignee);

            Consignment c = _getTestConsignment(consignor, 8, consignee);

            Consignment rc = ep.createConsignment(c);

            Assert.IsTrue(rc != null);
        }

        [TestMethod]
        public void getConsigneeTest_typecheck()
        {
            EPService.EDIPostService ep = new EPService.EDIPostService(ApiKey, ApiUrl);
            Consignment c = ep.getConsignment(ConsignmentId);

            Assert.IsTrue(c != null);
        }


        [TestMethod]
        public void getConsignmentTest_typecheck()
        {
            EPService.EDIPostService ep = new EPService.EDIPostService(ApiKey, ApiUrl);
            Consignment consignment = ep.getConsignment(ConsignmentId);

            Assert.IsTrue(consignment != null);
        }


        [TestMethod]
        public void PrintConsignment()
        {
            EPService.EDIPostService ep = new EPService.EDIPostService(ApiKey, ApiUrl);
            byte[] pdf = ep.printConsignment(ConsignmentId);

            string str = Encoding.Default.GetString(pdf);
            Assert.IsTrue( str.Substring(1,3) == "PDF" );
        }


        [TestMethod]
        public void PrintConsignmentZpl() {
            EPService.EDIPostService ep = new EPService.EDIPostService(ApiKey, ApiUrl);
            string zpl = ep.printConsignmentZpl(ConsignmentZplId);
            
            Assert.IsTrue(zpl.Substring(0, 3) == "^XA", "Data must start with ^XA");
        }


        [TestMethod]
        public void getPostageTest_checkPostage()
        {
            EPService.EDIPostService ep = new EPService.EDIPostService(ApiKey, ApiUrl);

            Consignor consignor = ep.getDefaultConsignor();

            Consignee testConsignee = _getTestConsignee();
            Consignee consignee = ep.createConsignee(testConsignee);

            Consignment testConsignment = this._getTestConsignment(consignor, 8, consignee);
            
            Consignment c = ep.getPostage(testConsignment);

            Assert.IsTrue((c.TotalPostageCost() > 0)); 
        }
      
        


        private Consignment _getTestConsignment(Consignor consignor, int productId = 0, Consignee consignee = null)
        {
            if ( consignee == null ) {
                consignee = _getTestConsignee(ConsigneeId);
            }

            Service eAlertMail = new Service
            {
                id = 5,
                properties = new Properties
                {
                    new Property("EMSG_SMS_NUMBER", "mail@mail.com")
                }
            };

            Service eAlertSms = new Service
            {
                id = 6,
                properties = new Properties
                {
                    new Property("EMSG_EMAIL", "mail@mail.com")
                }
            };

            EPBuilder.ConsignmentBuilder cb = new EPBuilder.ConsignmentBuilder
            {
                consigneeID = consignee.id,
                consignorID = consignor.id
            };

            cb.addItem(new Item(10, 20, 30, 40));
            cb.addItem(new Item(2.5, 25, 35, 45));
            cb.addService( eAlertMail );
            cb.addService(eAlertSms);
            

            if (productId > 0)
            {
                cb.productID = productId;
            }

            Consignment c = cb.build();

            return c;
        }
       

        private Consignee _getTestConsignee(int id = 0)
        {
            EPBuilder.ConsigneeBuilder cb = new EPBuilder.ConsigneeBuilder
            {
                companyName = "Test firma",
                customerNumber = "007",
                streetAddress = "Street address",
                streetCity = "StreetCity",
                streetZip = "2830",
                postAddress = "Postal address",
                postCity = "Post City",
                postZip = "2831",
                country = "NO",
                contactName = "Contact person",
                contactPhone = "611 59010",
                contactCellphone = "93 44 93 44",
                contactEmail = "teknisk@edipost.no"
            };

            if ( id > 0 ){
                cb.id = id;
            }

            Consignee c = cb.build();
            return c;
        }


        private Consignee _getTestConsignee_specialChars(int id = 0)
        {
            EPBuilder.ConsigneeBuilder cb = new EPBuilder.ConsigneeBuilder
            {
                companyName = "Nilsen & sønn",
                customerNumber = "008",
                streetAddress = "Calle ÆØÅ",
                streetCity = "ñunez",
                streetZip = "0987",
                postAddress = "Günter æøå",
                postCity = "Stoke ö ä å",
                postZip = "1234",
                country = "NO",
                contactName = "Kæøå PÆØÅ",
                contactPhone = "611 59010",
                contactCellphone = "93 44 93 44",
                contactEmail = "teknisk@edipost.no"
            };

            if (id > 0)
            {
                cb.id = id;
            }

            Consignee c = cb.build();
            return c;
        }

    }
}

