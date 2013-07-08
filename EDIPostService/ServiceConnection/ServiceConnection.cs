using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Xml;

namespace EDIPostService.ServiceConnection
{
    public class ServiceConnection
    {

        /// <summary>
        /// Holds the URL for EDIPost API interface
        /// </summary>
        private string _baseurl;
        public string baseurl
        {
            get
            {
                return this._baseurl;
            }
            set
            {
                this._baseurl = value;
            }
        }

        /// <summary>
        /// Holds the API key to identify the user
        /// </summary>
        private string _apikey;
        public string apikey
        {
            get
            {
                return this._apikey;
            }
            set
            {
                this._apikey = value;
            }
        }
        

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="base_url">EDIPost API url</param>
        /// <param name="api_key">EDIPost API key</param>
        public ServiceConnection(string base_url, string api_key)
        {
            baseurl = base_url;
            apikey = api_key;
        }



        public XmlDocument http_get(string url, string data = "", string headers = "")
        {
            return _getRequest(url, headers, data);
        }




        private XmlDocument _getRequest(string url, string headers = "", string data = "")
        {
            XmlDocument xml = new XmlDocument();

            String xml_string = "";
            String encoded = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("api:" + this.apikey));

            url = this.baseurl + url;
            
            try
            {
                
                HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
                req.Headers.Add("Authorization", "Basic " + encoded);


                
                using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(resp.GetResponseStream());
                    xml_string = reader.ReadToEnd();
                    xml.LoadXml(xml_string);
                }

            }
            catch (WebException e)
            {
                throw (e);
            }


            return xml;
            
        }

        
    }
}
