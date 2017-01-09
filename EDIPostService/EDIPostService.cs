using EDIPostService.Client;
using EDIPostService.Client.Builder;
using EDIPostService.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Antlr4.StringTemplate;
using EPService = EDIPostService.ServiceConnection;
using EPTools = EDIPostService.Tools;
using System.Globalization;



namespace EDIPostService
{

    /// <summary>
    /// An party enum for better readability
    /// </summary>
    enum PARTY : int { consignor=1, consignee=2, payer=7 };
    

    /// <summary>
    /// Api to connect to EDIPost
    /// </summary>
    public class EDIPostService : iEDIPostService
    {

        /// <summary>
        /// internal reference to the ServiceConnection
        /// </summary>
        private EPService.ServiceConnection sc { set; get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="api_key">The string that identifies the api user.</param>
        /// <param name="api_url">Url to the rest api</param>
        public EDIPostService(String api_key, String api_url = "http://api.edipost.no" )
        {
            sc  = new EPService.ServiceConnection(api_url, api_key);
        }

        /// <summary>
        /// Fetches a default consignor
        /// </summary>
        /// <returns>Consignor</returns>
        public Consignor getDefaultConsignor()
        {
            
            String path = "/consignor/default";
            XmlDocument data = null;
            String accept = "application/vnd.edipost.party+xml";
            List<String> headers = new List<string>();

            // Fetches the data 
            XmlDocument xml = sc.http_get(path, data, headers, accept);

            return _buildConsignor(xml);
        }

        /// <summary>
        /// Find the postage for the current consignment
        /// </summary>
        /// <param name="consignment">The consignment we want the postage for</param>
        /// <returns>Consignment</returns>
        public Consignment getPostage(Consignment consignment)
        {
            Template path = new Template("/consignment/postage");
            string accept = "application/vnd.edipost.consignment+xml";
            string contenttype = "application/vnd.edipost.consignment+xml";
            List<String> headers = new List<string>();

            string url = path.Render();

            XmlDocument data = EPTools.xml.format<Consignment>(consignment, true);

            XmlDocument xml = sc.http_post(url, data, headers, accept, contenttype);
            
            Consignment c = this._buildConsignment(xml);
            return c;
        }


        /// <summary>
        /// Fetches all available products
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public List<Product> findProducts(Consignment c)
        {
            Template path = new Template("/consignee/<consigneeId>/products?<items>");
            XmlDocument data = null;
            string accept = "application/vnd.edipost.collection+xml";
            string contenttype = null;
            List<String> headers = new List<string>();
            
            path.Add("consigneeId", c.consignee.id);
            path.Add("items", "");

            

            string url = path.Render();

            XmlDocument xml = sc.http_get(url, data, headers, accept, contenttype);

            List<Product> products = _buildProduct(xml);

            

            return products;
        }


        /// <summary>
        /// Create the consignment in EDIPost
        /// </summary>
        /// <param name="consignment">The consignment to create</param>
        /// <returns>the created consignment</returns>
        public Consignment createConsignment(Consignment consignment) {
            string path = "/consignment";
            XmlDocument xml = null;
            string accept = "application/vnd.edipost.consignment+xml";
            string contenttype = "application/vnd.edipost.consignment+xml";

            XmlDocument data = EPTools.xml.format<Consignment>(consignment, true);

            // Error prechecks
            if ( consignment.consignee.id == null)
            {
                throw new DataException("Missing Consignee ID");
            }

            if (consignment.product.id == null)
            {
                throw new DataException("Missing Product ID");
            }

                       
            try
            {
                xml = sc.http_post(path, data, null, accept, contenttype);

                if (xml.GetType() != typeof(XmlDocument))
                {
                    throw new DataException("Invalid return data.");
                }
            }
            catch (EPService.Exceptions.ConnectionException ce)
            {
                throw ce;
            }
              
            Consignment c = this._buildConsignment(xml);

            return c;
        }
        
        
        

        /// <summary>
        /// Creates a consignee party ( recipient )
        /// </summary>
        /// <param name="consignee"></param>
        /// <returns>Consignee</returns>
        public Consignee createConsignee(Consignee consignee)
        {
            string path = "/consignee";
            XmlDocument xml = null;
            string accept = "application/vnd.edipost.party+xml";
            string contenttype = "application/vnd.edipost.party+xml";
            
            XmlDocument data = EPTools.xml.format<Consignee>(consignee, true);

            // Error precheck
            if (String.IsNullOrEmpty(consignee.companyName))
            {
                throw new DataException("Consignee CompanyName cannot be empty");
            }
            if (String.IsNullOrEmpty(consignee.postAddress.zipCode))
            {
                throw new DataException("Consignee PostAddress zipCode cannot be empty");
            }
            if (String.IsNullOrEmpty(consignee.streetAddress.zipCode))
            {
                throw new DataException("Consignee StreetAddress zipCode cannot be empty");
            }



            // Do the actual api call        
            try
            {
                xml = sc.http_post(path, data, null, accept, contenttype);

                if (xml.GetType() != typeof(XmlDocument))
                {
                    throw new DataException("Invalid return data.");
                }
            }
            catch (EPService.Exceptions.ConnectionException ce)
            {
                throw ce;
            }
             
            return _buildConsignee(xml);

        }



        /// <summary>
        /// Prints the consignment and return a BAE64 encoded representation of the PDF file
        /// </summary>
        /// <param name="id">The id of the consignment to print</param>
        /// <returns>a base64 encoded string containing the PDF</returns>
        public byte[] printConsignment(int id)
        {
            Antlr4.StringTemplate.Template path = new Antlr4.StringTemplate.Template("/consignment/<consignmentId>/print");
            string accept = "application/pdf";
            string contenttype = "application/pdf";
            List<String> headers = new List<string>();

            path.Add("consignmentId", id);
            string url = path.Render();

            byte[] data = sc.http_get_raw(url, accept, contenttype );

            return data;
        }

        /// <summary>
        /// Fetches a consignment from the archive
        /// </summary>
        /// <param name="id">id of the consignment to get</param>
        /// <returns>consignment</returns>
        public Consignment getConsignment(int id)
        {
            Antlr4.StringTemplate.Template path = new Antlr4.StringTemplate.Template("/consignment/<consignmentId>");
            string accept = "application/vnd.edipost.consignment+xml";
            string contenttype = null;
            List<String> headers = new List<string>();

            path.Add("consignmentId", id);
            string url = path.Render();

            XmlDocument xml = sc.http_get(url, null, null, accept, contenttype);

            Consignment c = _buildConsignment(xml);

            return c;

        }

        
        /// <summary>
        /// Finds consignements from the archive based on a search criterea
        /// </summary>
        /// <param name="query">The query string</param>
        /// <returns>List of consignments</returns>
        public List<Consignment> searchConsignment(string query)
        {
               
            return new List<Consignment>();
        }

        /// <summary>
        /// Finds consignee from the EDIPost addressbook
        /// </summary>
        /// <remarks>The list of consignees only contain ID and companyName, this one need to be extended to contain all</remarks>
        /// <param name="query">the search query</param>
        /// <param name="start_at">Start resultset at position</param>
        /// <param name="return_count">How many to return from the resultset</param>
        /// <returns>List of consigees</returns>
        public List<Consignee> searchConsignee(string query, int start_at = 1, int return_count = 25)
        {
            Antlr4.StringTemplate.Template path = new Antlr4.StringTemplate.Template("/consignee/search?q=<query>&start=<start_at>&count=<return_count>");
            string accept = "application/vnd.edipost.collection+xml";
            string contenttype = null;
            List<String> headers = new List<string>();

            path.Add("query", query);
            path.Add("start_at", start_at.ToString() );
            path.Add("return_count", return_count.ToString() );
            
            string url = path.Render();

            XmlDocument xml = sc.http_get(url, null, null, accept, contenttype);

            List<Consignee> c =  new List<Consignee>();
            
            XmlNodeList item_list = xml.SelectNodes("//collection/entry");
            foreach (XmlNode item in item_list)
            {
                Consignee consignee = new Consignee();
                consignee.id = Convert.ToInt32(EPTools.xml.nodeValue(item, "@id", true));
                consignee.companyName = EPTools.xml.nodeValue(item, "companyName");
                c.Add(consignee);
            }

            return c;
        }

        /// <summary>
        /// Fetches a consignee on ID
        /// </summary>
        /// <param name="id">id of the consignment</param>
        /// <returns>the consignee</returns>
        public Consignee getConsignee(int id)
        {
            Antlr4.StringTemplate.Template path = new Antlr4.StringTemplate.Template("/consignee/<consigneeId>");
            string accept = "application/vnd.edipost.party+xml";
            string contenttype = null;
            List<String> headers = new List<string>();

            path.Add("consigneeId", id);
            string url = path.Render();

            XmlDocument xml = sc.http_get(url, null, null, accept, contenttype);

            Consignee c = _buildConsignee(xml);

            return c;
        }


        


        #region Private methods

        /// <summary>
        /// Builds a Consignment based on XML and with the help of ConsignmentBuilder
        /// </summary>
        /// <param name="xml">The XmlDocument containing the consignment</param>
        /// <returns>The consignment</returns>
        protected Consignment _buildConsignment(XmlDocument xml)
        {
            Consignment c = new Consignment();
            Consignor consignor = new Consignor();
            Consignee consignee = new Consignee();


            // ConsignmentDetails
            if (xml.SelectSingleNode("/consignment/@id") != null)
            {
                c.id = Convert.ToInt32(EPTools.xml.nodeValue(xml, "/consignment/@id", true));
            }
            c.shipmentNumber = EPTools.xml.nodeValue(xml, "/consignment/shipmentNumber");
            c.contentReference = EPTools.xml.nodeValue(xml, "/consignment/contentReference");
            c.transportInstructions = EPTools.xml.nodeValue(xml, "/consignment/transportInstructions");
            c.internalReference = EPTools.xml.nodeValue(xml, "/consignment/internalReference");

            c.trackAndTraceUrl = EPTools.xml.nodeValue(xml, "*/links/link[@rel='trackandtrace-gui']/@href");

            // Consignor
            c.consignor = this._buildConsignor(xml);

            // Consignee
            c.consignee = this._buildConsignee(xml);

            // Product
            c.product = this._buildProduct(xml, true);

            // Items
            c.items = new Items();
            XmlNodeList item_list = xml.SelectNodes("/consignment/items/item");
            foreach (XmlNode item in item_list)
            {
                Item i = new Item();

                // Add the itemNumber if exists
                if (item.SelectSingleNode("itemNumber") != null)
                {
                    i.itemNumber = EPTools.xml.nodeValue(item, "itemNumber");
                }

                // Add the cost as double if exists
                if (item.SelectSingleNode("cost") != null)
                {
                    i.cost = Convert.ToDouble(EPTools.xml.nodeValue(item, "cost", true), CultureInfo.InvariantCulture);
                }
                
                // Add Vat as double if exists
                if (item.SelectSingleNode("vat") != null)
                {
                    i.vat = Convert.ToDouble(EPTools.xml.nodeValue(item, "vat", true), CultureInfo.InvariantCulture);
                }


                i.weight = Convert.ToDouble(EPTools.xml.nodeValue(item, "weight", true), CultureInfo.InvariantCulture);
                i.length = Convert.ToDouble(EPTools.xml.nodeValue(item, "length", true), CultureInfo.InvariantCulture);
                i.height = Convert.ToDouble(EPTools.xml.nodeValue(item, "height", true), CultureInfo.InvariantCulture);
                i.width = Convert.ToDouble(EPTools.xml.nodeValue(item, "width", true), CultureInfo.InvariantCulture);
                
                c.items.Add(i);
            }

            return c;
        }
        

        /// <summary>
        /// Builds a single object based on any XML containing a Product tag.
        /// </summary>
        /// <param name="xml">The XML to parse</param>
        /// <param name="single">to tell that it is a single product</param>
        /// <returns>Product object</returns>
        protected Product _buildProduct(XmlDocument xml, bool single)
        {
            ProductBuilder pb = new ProductBuilder();
            pb.id = Convert.ToInt32(EPTools.xml.nodeValue(xml, "*/product/@id", true));
            pb.name = EPTools.xml.nodeValue(xml, "*/product/@name");
            pb.transportername = EPTools.xml.nodeValue(xml, "*/product/transporter/@name");
            pb.status = EPTools.xml.nodeValue(xml, "*/product/transporter/status");

            XmlNodeList service_list = xml.SelectNodes("*/product/services/service");
            foreach (XmlNode service in service_list)
            {
                Service s = new Service();
                s.id = Convert.ToInt32(EPTools.xml.nodeValue(service, "@id", true));
                s.name = EPTools.xml.nodeValue(service, "@name");
                if (service.SelectSingleNode("cost") != null)
                {
                    s.cost = Convert.ToDouble(EPTools.xml.nodeValue(service, "cost", true));
                }
                if (service.SelectSingleNode("vat") != null)
                {
                    s.vat = Convert.ToDouble(EPTools.xml.nodeValue(service, "vat", true));
                }

                pb.addService(s);
            }

            return pb.build();
        }
    
        /// <summary>
        /// Internal method to build an list of products
        /// </summary>
        /// <param name="xml">the xmldocument containing the products</param>
        /// <returns>List of products</returns>
        protected List<Product> _buildProduct(XmlDocument xml)
        {
            List<Product> products = new List<Product>();
            XmlNodeList nl = xml.SelectNodes("//collection/entry");

            foreach (XmlNode n in nl)
            {
                ProductBuilder pb = new ProductBuilder();
                pb.id = Convert.ToInt32( EPTools.xml.nodeValue(n, "@id", true) );
                pb.name = EPTools.xml.nodeValue(n, "@name");
                pb.transportername = EPTools.xml.nodeValue(n, "transporter/@name");

                XmlNodeList service_list = n.SelectNodes("services/service");
                foreach (XmlNode service in service_list)
                {
                    Service s = new Service();
                    s.id = Convert.ToInt32(EPTools.xml.nodeValue(service, "@id", true));
                    s.name = EPTools.xml.nodeValue(service, "@name");

                    pb.addService(s);
                }

                products.Add(pb.build());
            }

            return products;

        }

        /// <summary>
        /// Builds a Consignor object based on xml
        /// </summary>
        /// <param name="xml">XmlDocument containing consignor data</param>
        /// <returns>Consignor</returns>
        private Consignor _buildConsignor(XmlDocument xml)
        {
            ConsignorBuilder cb = new ConsignorBuilder();
            cb.id = Convert.ToInt32(xml.SelectSingleNode("//consignor/@id").Value);
            cb.companyName = ( xml.SelectSingleNode("//consignor/companyName") != null ? xml.SelectSingleNode("//consignor/companyName").InnerText : "");
            cb.customerNumber = ( xml.SelectSingleNode("//consignor/customerNumber") != null ? xml.SelectSingleNode("//consignor/customerNumber").InnerText : "" );
            cb.postAddress = ( xml.SelectSingleNode("//consignor/postAddress/address") != null ? xml.SelectSingleNode("//consignor/postAddress/address").InnerText : "" );
            cb.postZip = ( xml.SelectSingleNode("//consignor/postAddress/zipCode") != null ? xml.SelectSingleNode("//consignor/postAddress/zipCode").InnerText : "" );
            cb.postCity = ( xml.SelectSingleNode("//consignor/postAddress/city") != null ? xml.SelectSingleNode("//consignor/postAddress/city").InnerText : "" );
            cb.streetAddress = ( xml.SelectSingleNode("//consignor/streetAddress/address") != null ? xml.SelectSingleNode("//consignor/streetAddress/address").InnerText : "");
            cb.streetZip = ( xml.SelectSingleNode("//consignor/streetAddress/zipCode") != null ? xml.SelectSingleNode("//consignor/streetAddress/zipCode").InnerText : "");
            cb.streetCity = ( xml.SelectSingleNode("//consignor/streetAddress/city") != null ? xml.SelectSingleNode("//consignor/streetAddress/city").InnerText : "" );
            cb.contactName = ( xml.SelectSingleNode("//consignor/contact/name") != null ? xml.SelectSingleNode("//consignor/contact/name").InnerText : "" );
            cb.contactEmail = ( xml.SelectSingleNode("//consignor/contact/email") != null ? xml.SelectSingleNode("//consignor/contact/email").InnerText : "");
            cb.contactPhone = ( xml.SelectSingleNode("//consignor/contact/telephone") != null ? xml.SelectSingleNode("//consignor/contact/telephone").InnerText : "");
            cb.contactCellphone = ( xml.SelectSingleNode("//consignor/contact/cellphone") != null ? xml.SelectSingleNode("//consignor/contact/cellphone").InnerText : "" );
            cb.country = xml.SelectSingleNode("//consignor/country").InnerText;

            return cb.build();

        }



        /// <summary>
        /// Builds a Consignee object
        /// </summary>
        /// <param name="xml"></param>
        /// <returns>Congsignee</returns>
        private Consignee _buildConsignee(XmlDocument xml)
        {
            ConsigneeBuilder cb = new ConsigneeBuilder();
            cb.id = Convert.ToInt32(xml.SelectSingleNode("//consignee/@id").Value);
            cb.companyName = (xml.SelectSingleNode("//consignee/companyName") != null ? xml.SelectSingleNode("//consignee/companyName").InnerText : "");
            cb.customerNumber = (xml.SelectSingleNode("//consignee/customerNumber") != null ? xml.SelectSingleNode("//consignee/customerNumber").InnerText : "");
            cb.postAddress = (xml.SelectSingleNode("//consignee/postAddress/address") != null ? xml.SelectSingleNode("//consignee/postAddress/address").InnerText : "");
            cb.postZip = (xml.SelectSingleNode("//consignee/postAddress/zipCode") != null ? xml.SelectSingleNode("//consignee/postAddress/zipCode").InnerText : "");
            cb.postCity = (xml.SelectSingleNode("//consignee/postAddress/city") != null ? xml.SelectSingleNode("//consignee/postAddress/city").InnerText : "");
            cb.streetAddress = (xml.SelectSingleNode("//consignee/streetAddress/address") != null ? xml.SelectSingleNode("//consignee/streetAddress/address").InnerText : "");
            cb.streetZip = (xml.SelectSingleNode("//consignee/streetAddress/zipCode") != null ? xml.SelectSingleNode("//consignee/streetAddress/zipCode").InnerText : "");
            cb.streetCity = (xml.SelectSingleNode("//consignee/streetAddress/city") != null ? xml.SelectSingleNode("//consignee/streetAddress/city").InnerText : "");
            cb.contactName = (xml.SelectSingleNode("//consignee/contact/name") != null ? xml.SelectSingleNode("//consignee/contact/name").InnerText : "");
            cb.contactEmail = (xml.SelectSingleNode("//consignee/contact/email") != null ? xml.SelectSingleNode("//consignee/contact/email").InnerText : "");
            cb.contactPhone = (xml.SelectSingleNode("//consignee/contact/telephone") != null ? xml.SelectSingleNode("//consignee/contact/telephone").InnerText : "");
            cb.contactCellphone = (xml.SelectSingleNode("//consignee/contact/cellphone") != null ? xml.SelectSingleNode("//consignee/contact/cellphone").InnerText : "");
            cb.country = xml.SelectSingleNode("//consignee/country").InnerText;

            return cb.build();
        }



        # endregion


    }
}
