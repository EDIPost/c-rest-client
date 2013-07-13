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
//using System.Xml.Serialization;
using EPService = EDIPostService.ServiceConnection;
using EPTools = EDIPostService.Tools;



namespace EDIPostService
{
    enum PARTY : int { consignor=1, consignee=2, payer=7 };

    public class EDIPostService : iEDIPostService
    {

        public EPService.ServiceConnection sc { set; get; }

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

            /// Fetches the data 
            XmlDocument xml = sc.http_get(path, data, headers, accept);

            return _buildConsignor(xml);
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





        #region Private methods

        /// <summary>
        /// Builds a Consignor object based on xml
        /// </summary>
        /// <param name="xml">XmlDocument containing consignor data</param>
        /// <returns>Consignor</returns>
        private Consignor _buildConsignor(XmlDocument xml)
        {
            ConsignorBuilder cb = new ConsignorBuilder();
            cb.id = Convert.ToInt32(xml.SelectSingleNode("/consignor/@id").Value);
            cb.companyName = ( xml.SelectSingleNode("/consignor/companyName") != null ? xml.SelectSingleNode("/consignor/companyName").InnerText : "");
            cb.customerNumber = ( xml.SelectSingleNode("/consignor/customerNumber") != null ? xml.SelectSingleNode("/consignor/customerNumber").InnerText : "" );
            cb.postAddress = ( xml.SelectSingleNode("/consignor/postAddress/address") != null ? xml.SelectSingleNode("/consignor/postAddress/address").InnerText : "" );
            cb.postZip = ( xml.SelectSingleNode("/consignor/postAddress/zipCode") != null ? xml.SelectSingleNode("/consignor/postAddress/zipCode").InnerText : "" );
            cb.postCity = ( xml.SelectSingleNode("/consignor/postAddress/city") != null ? xml.SelectSingleNode("/consignor/postAddress/city").InnerText : "" );
            cb.streetAddress = ( xml.SelectSingleNode("/consignor/streetAddress/address") != null ? xml.SelectSingleNode("/consignor/streetAddress/address").InnerText : "");
            cb.streetZip = ( xml.SelectSingleNode("/consignor/streetAddress/zipCode") != null ? xml.SelectSingleNode("/consignor/streetAddress/zipCode").InnerText : "");
            cb.streetCity = ( xml.SelectSingleNode("/consignor/streetAddress/city") != null ? xml.SelectSingleNode("/consignor/streetAddress/city").InnerText : "" );
            cb.contactName = ( xml.SelectSingleNode("/consignor/contact/name") != null ? xml.SelectSingleNode("/consignor/contact/name").InnerText : "" );
            cb.contactEmail = ( xml.SelectSingleNode("/consignor/contact/email") != null ? xml.SelectSingleNode("/consignor/contact/email").InnerText : "");
            cb.contactPhone = ( xml.SelectSingleNode("/consignor/contact/telephone") != null ? xml.SelectSingleNode("/consignor/contact/telephone").InnerText : "");
            cb.contactCellphone = ( xml.SelectSingleNode("/consignor/contact/cellphone") != null ? xml.SelectSingleNode("/consignor/contact/cellphone").InnerText : "" );
            cb.country = xml.SelectSingleNode("/consignor/country").InnerText;

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
            cb.id = Convert.ToInt32(xml.SelectSingleNode("/consignee/@id").Value);
            cb.companyName = (xml.SelectSingleNode("/consignee/companyName") != null ? xml.SelectSingleNode("/consignee/companyName").InnerText : "");
            cb.customerNumber = (xml.SelectSingleNode("/consignee/customerNumber") != null ? xml.SelectSingleNode("/consignee/customerNumber").InnerText : "");
            cb.postAddress = (xml.SelectSingleNode("/consignee/postAddress/address") != null ? xml.SelectSingleNode("/consignee/postAddress/address").InnerText : "");
            cb.postZip = (xml.SelectSingleNode("/consignee/postAddress/zipCode") != null ? xml.SelectSingleNode("/consignee/postAddress/zipCode").InnerText : "");
            cb.postCity = (xml.SelectSingleNode("/consignee/postAddress/city") != null ? xml.SelectSingleNode("/consignee/postAddress/city").InnerText : "");
            cb.streetAddress = (xml.SelectSingleNode("/consignee/streetAddress/address") != null ? xml.SelectSingleNode("/consignee/streetAddress/address").InnerText : "");
            cb.streetZip = (xml.SelectSingleNode("/consignee/streetAddress/zipCode") != null ? xml.SelectSingleNode("/consignee/streetAddress/zipCode").InnerText : "");
            cb.streetCity = (xml.SelectSingleNode("/consignee/streetAddress/city") != null ? xml.SelectSingleNode("/consignee/streetAddress/city").InnerText : "");
            cb.contactName = (xml.SelectSingleNode("/consignee/contact/name") != null ? xml.SelectSingleNode("/consignee/contact/name").InnerText : "");
            cb.contactEmail = (xml.SelectSingleNode("/consignee/contact/email") != null ? xml.SelectSingleNode("/consignee/contact/email").InnerText : "");
            cb.contactPhone = (xml.SelectSingleNode("/consignee/contact/telephone") != null ? xml.SelectSingleNode("/consignee/contact/telephone").InnerText : "");
            cb.contactCellphone = (xml.SelectSingleNode("/consignee/contact/cellphone") != null ? xml.SelectSingleNode("/consignee/contact/cellphone").InnerText : "");
            cb.country = xml.SelectSingleNode("/consignee/country").InnerText;

            return cb.build();
        }



        # endregion


    }
}
