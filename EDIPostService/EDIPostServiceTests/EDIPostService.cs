using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using EPService = EDIPostService;
using EPClient = EDIPostService.Client;
using EPBuilder = EDIPostService.Client.Builder;
using System.Net;
using System.Threading;
using EDIPostService.Client;
using EDIPostService.ServiceConnection.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace EDIPostServiceTests
{
    [TestClass]
    public class EDIPostService
    {
        public const String API_URL = "http://api.dev.edipost.no/";
        public const String API_KEY = "07add61e089e3e8d3a1a7e34e71f462eee2ef8f5";
        public const int DEFAULT_CONSIGNOR_ID = 3311;
        public const int CONSIGNEE_ID = 3270125;
        public const int CONSIGNMENT_ID = 3331708;
        public const int CONSIGNMENT_ZPL_ID = 3334708;


        [TestMethod]
        public void EDIPostServiceTest_OptionalConstructorParam()
        {
            EPService.EDIPostService ep = new EPService.EDIPostService("213131");

            Assert.IsFalse(false);
        }

        [TestMethod]
        public void GetDefaultConsignorTest_correct_type()
        {
            EPService.EDIPostService ep = new EPService.EDIPostService(API_KEY, API_URL);
            EPClient.Consignor c = ep.getDefaultConsignor();

            Assert.IsInstanceOfType(c, typeof(EPClient.Consignor));
        }


        [TestMethod]
        public void GetDefaultConsignorTest_correct_data()
        {
            EPService.EDIPostService ep = new EPService.EDIPostService(API_KEY, API_URL);
            EPClient.Consignor c = ep.getDefaultConsignor();

            Assert.AreEqual(DEFAULT_CONSIGNOR_ID, c.id);
        }


        [TestMethod]
        public void createConsigneeTest_domestic_request()
        {
            EPService.EDIPostService ep = new EPService.EDIPostService(API_KEY, API_URL);

            // Create the consignee object
            EPClient.Consignee c = _getTestConsignee();
            EPClient.Consignee rc = ep.createConsignee(c);

            Assert.IsTrue(rc is EPClient.Consignee);
        }


        [TestMethod]
        public void createConsigneeTest_specialChars()
        {
            EPService.EDIPostService ep = new EPService.EDIPostService(API_KEY, API_URL);

            // Create the consignee object
            EPClient.Consignee c = _getTestConsignee_specialChars();
            EPClient.Consignee rc = ep.createConsignee(c);

            Assert.IsTrue(rc is EPClient.Consignee);
        }


        [TestMethod]
        public void removeConsigneeTest() {
            EPService.EDIPostService ep = new EPService.EDIPostService(API_KEY, API_URL);

            // Create the consignee object
            EPClient.Consignee c = _getTestConsignee();
            EPClient.Consignee rc = ep.createConsignee(c);

            ep.removeConsignee(rc.id);
        }


        [TestMethod]
        [ExpectedException(typeof(HttpException), "It should not be possible to remove a non-exsiting consignee ID")]
        public void removeConsigneeNonExistingConsingneeTest() {
            EPService.EDIPostService ep = new EPService.EDIPostService(API_KEY, API_URL);

            ep.removeConsignee(987654321);  // Non-existing ID
        }


        [TestMethod]
        public void searchConsigneeTest_type()
        {
            EPService.EDIPostService ep = new EPService.EDIPostService(API_KEY, API_URL);
            List<Consignee> consignees = ep.searchConsignee("Folco", 1, 25);

            Assert.IsTrue(consignees.Count > 0, "Less than 1 consignees returned");

            Assert.IsTrue(consignees[0] is EPClient.Consignee, "Result set has wrong type");
        }


        [TestMethod]
        public void findProductTest_domestic()
        {
            EPService.EDIPostService ep = new EPService.EDIPostService(API_KEY, API_URL);

            EPClient.Consignee testConsignee = _getTestConsignee();
            EPClient.Consignee consignee = ep.createConsignee(testConsignee);

            EPClient.Consignor consignor = ep.getDefaultConsignor();

            EPClient.Items items = new EPClient.Items();

            EPBuilder.ConsignmentBuilder cb = new EPBuilder.ConsignmentBuilder();
            cb.consigneeID = consignee.id;
            cb.consignorID = consignor.id;
            cb.addItem(new EPClient.Item(10, 20, 30, 40));
            cb.addItem(new EPClient.Item(2.5, 25, 35, 45));
            
            
            EPClient.Consignment c = cb.build();
            
            List<EPClient.Product> products = ep.findProducts(c);

            Assert.IsTrue(products.Count > 0);
        }


        [TestMethod]
        public void findAllProductsTest()
        {
            EPService.EDIPostService ep = new EPService.EDIPostService(API_KEY, API_URL);

            List<EPClient.Product> products = ep.findAllProducts();
            products.ForEach(Console.WriteLine);

            Assert.IsTrue(products.Count > 0);
        }


        [TestMethod]
        public void createConsignmentTest_returntype()
        {
            EPService.EDIPostService ep = new EPService.EDIPostService(API_KEY, API_URL);
            EPClient.Consignor consignor = ep.getDefaultConsignor();

            EPClient.Consignee testConsignee = _getTestConsignee();
            EPClient.Consignee consignee = ep.createConsignee(testConsignee);

            Consignment c = _getTestConsignment(consignor, 8, consignee);

            Consignment rc = ep.createConsignment(c);

            Assert.IsTrue(rc is EPClient.Consignment);
        }

        [TestMethod]
        public void getConsigneeTest_typecheck()
        {
            EPService.EDIPostService ep = new EPService.EDIPostService(API_KEY, API_URL);
            Consignment c = ep.getConsignment(CONSIGNMENT_ID);

            Assert.IsTrue(c is EPClient.Consignment);
        }


        [TestMethod]
        public void getConsignmentTest_typecheck()
        {
            EPService.EDIPostService ep = new EPService.EDIPostService(API_KEY, API_URL);
            EPClient.Consignment consignment = ep.getConsignment(CONSIGNMENT_ID);

            Assert.IsTrue(consignment is EPClient.Consignment);
        }


        [TestMethod]
        public void printConsignment()
        {
            EPService.EDIPostService ep = new EPService.EDIPostService(API_KEY, API_URL);
            byte[] pdf = ep.printConsignment(CONSIGNMENT_ID);

            string str = System.Text.Encoding.Default.GetString(pdf);
            Assert.IsTrue( str.Substring(1,3) == "PDF" );
        }


        [TestMethod]
        public void printConsignmentZpl() {
            EPService.EDIPostService ep = new EPService.EDIPostService(API_KEY, API_URL);
            string zpl = ep.printConsignmentZpl(CONSIGNMENT_ZPL_ID);
            
            Assert.IsTrue(zpl.Substring(0, 3) == "^XA", "Data must start with ^XA");
        }


        [TestMethod]
        public void getPostageTest_checkPostage()
        {
            EPService.EDIPostService ep = new EPService.EDIPostService(API_KEY, API_URL);

            EPClient.Consignor consignor = ep.getDefaultConsignor();

            EPClient.Consignee testConsignee = _getTestConsignee();
            EPClient.Consignee consignee = ep.createConsignee(testConsignee);

            EPClient.Consignment test_consignment = this._getTestConsignment(consignor, 8, consignee);
            
            EPClient.Consignment c = ep.getPostage(test_consignment);

            Assert.IsTrue((c.TotalPostageCost() > 0)); 
        }
      
        


        private Consignment _getTestConsignment(Consignor consignor, int productId = 0, Consignee consignee = null)
        {
            if ( consignee == null )
                consignee = _getTestConsignee(CONSIGNEE_ID);

            EPClient.Items items = new EPClient.Items();

            Service eAlertMail = new Service();
            eAlertMail.id = 5;
            eAlertMail.properties = new Properties();
            eAlertMail.properties.Add( new Property("EMSG_SMS_NUMBER", "mail@mail.com") );

            Service eAlertSms = new Service();
            eAlertSms.id = 6;
            eAlertSms.properties = new Properties();
            eAlertSms.properties.Add( new Property("EMSG_EMAIL", "mail@mail.com") );

            EPBuilder.ConsignmentBuilder cb = new EPBuilder.ConsignmentBuilder();
            cb.consigneeID = consignee.id;
            cb.consignorID = consignor.id;
            cb.addItem(new EPClient.Item(10, 20, 30, 40));
            cb.addItem(new EPClient.Item(2.5, 25, 35, 45));
            cb.addService( eAlertMail );
            cb.addService(eAlertSms);
            

            if (productId > 0)
            {
                cb.productID = productId;
            }

            EPClient.Consignment c = cb.build();

            return c;
        }
       

        private EPClient.Consignee _getTestConsignee(int id = 0)
        {
            EPBuilder.ConsigneeBuilder cb = new EPBuilder.ConsigneeBuilder();
            cb.companyName = "Test firma";
            cb.customerNumber = "007";
            cb.streetAddress = "Street address";
            cb.streetCity = "StreetCity";
            cb.streetZip = "2830";
            cb.postAddress = "Postal address";
            cb.postCity = "Post City";
            cb.postZip = "2831";
            cb.country = "NO";
            cb.contactName = "Contact person";
            cb.contactPhone = "611 59010";
            cb.contactCellphone = "93 44 93 44";
            cb.contactEmail = "teknisk@edipost.no";

            if ( id > 0 ){
                cb.id = id;
            }

            EPClient.Consignee c = cb.build();
            return c;
        }


        private EPClient.Consignee _getTestConsignee_specialChars(int id = 0)
        {
            EPBuilder.ConsigneeBuilder cb = new EPBuilder.ConsigneeBuilder();
            cb.companyName = "Nilsen & sønn";
            cb.customerNumber = "008";
            cb.streetAddress = "Calle ÆØÅ";
            cb.streetCity = "ñunez";
            cb.streetZip = "0987";
            cb.postAddress = "Günter æøå";
            cb.postCity = "Stoke ö ä å";
            cb.postZip = "1234";
            cb.country = "NO";
            cb.contactName = "Kæøå PÆØÅ";
            cb.contactPhone = "611 59010";
            cb.contactCellphone = "93 44 93 44";
            cb.contactEmail = "teknisk@edipost.no";

            if (id > 0)
            {
                cb.id = id;
            }

            EPClient.Consignee c = cb.build();
            return c;
        }

    }
}

