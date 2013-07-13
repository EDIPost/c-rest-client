﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Xml;

namespace EDIPostService.ServiceConnection
{

    /// <summary>
    /// Defines internal constants for better readability
    /// </summary>
    public static class Constants
    {
        public const string _post_ = "POST";
        public const string _get_ = "GET";
    }

    public class ServiceConnection
    {
        #region properties
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
        # endregion

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




        /// <summary>
        /// Public method to handle GET requests
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="accept"></param>
        /// <param name="contenttype"></param>
        /// <returns></returns>
        public XmlDocument http_get(string url, XmlDocument data = null, List<String> headers = null, string accept = null, string contenttype = null )
        {
            return _getRequest(url, headers, data, accept, contenttype);
        }



        /// <summary>
        /// Public method to do the POST request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="accept"></param>
        /// <param name="contenttype"></param>
        /// <returns>XmlDocument</returns>
        public XmlDocument http_post(string url, XmlDocument data = null, List<String> headers = null, string accept = null, string contenttype = null)
        {
            return _postRequest(url, headers, data, accept, contenttype);
        }




        /// <summary>
        /// Internal method to handle POST requests.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="data"></param>
        /// <param name="accept"></param>
        /// <returns></returns>
        private XmlDocument _postRequest(string url, List<String> headers = null, XmlDocument data = null, string accept = null, string contenttype = null)
        {
            return _handleRequest( url, Constants._post_, headers, data, accept, contenttype );
        }





        /// <summary>
        /// Internal method to handle GET requests
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="data"></param>
        /// <param name="accept"></param>
        /// <returns></returns>
        private XmlDocument _getRequest(string url, List<String> headers = null, XmlDocument data = null, string accept = null, string contenttype = null )
        {
            return _handleRequest( url, Constants._get_, headers, data, accept, contenttype );
        }


        
        
        
        
        /// <summary>
        /// Internal method to handle all kind of requests sendt to the API.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="headers"></param>
        /// <param name="data"></param>
        /// <param name="accept"></param>
        /// <param name="contenttype"></param>
        /// <returns>XmlDocument</returns>
        private XmlDocument _handleRequest(string url, string method = Constants._get_, List<String> headers = null, 
                                                XmlDocument data = null, string accept = null, string contenttype = null)
        {
            XmlDocument xml = new XmlDocument();
            String encoded = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("api:" + this.apikey));

            // Builds the URL
            url = this.baseurl + url.Replace("//", "/");

            try
            {

                // Set up a Request object
                HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
                req.Headers.Add("Authorization", "Basic " + encoded);


                // Add the Accept header
                if (accept != null)
                {
                    req.Accept = accept;
                }


                // Add ContentType header
                if (contenttype != null)
                {
                    req.ContentType = contenttype;
                }


                // Add additional headers
                if (headers != null)
                {
                    foreach (string h in headers)
                    {
                        req.Headers.Add(h);
                    }
                }

                
                req.Method = method;
                req.KeepAlive = true;


                // Handle data to be sendt
                if (data != null)
                {


                    string data_string = "";
                    using (var stringWriter = new StringWriter())
                    using (var xmlTextWriter = XmlWriter.Create(stringWriter))
                    {
                        data.WriteTo(xmlTextWriter);
                        xmlTextWriter.Flush();
                        data_string = stringWriter.GetStringBuilder().ToString();
                    }



                    MemoryStream sw = new MemoryStream();

                    byte[] buffer = Encoding.ASCII.GetBytes(data_string);
                    req.ContentLength = buffer.Length;

                    Stream PostData = req.GetRequestStream();
                    PostData.Write(buffer, 0, buffer.Length);
                    PostData.Close();
                }
                
                HttpWebResponse WebResp = (HttpWebResponse)req.GetResponse();
                Stream Answer = WebResp.GetResponseStream();
                StreamReader response = new StreamReader(Answer);

                xml.LoadXml(response.ReadToEnd());

            }
            catch (Exception e)
            {
                throw new Exceptions.ConnectionException(e.Message, e.InnerException);

            }

            return xml;
        }

        
    }
}
