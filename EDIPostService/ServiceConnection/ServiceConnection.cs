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

    /// <summary>
    /// Defines internal constants for better readability
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// A constant to define POST method
        /// </summary>
        public const string _post_ = "POST";

        /// <summary>
        /// Constant to define GET method
        /// </summary>
        public const string _get_ = "GET";
    }


    /// <summary>
    /// Class that manaage all lower level communication with the API
    /// </summary>
    public class ServiceConnection
    {
        #region properties
        /// <summary>
        /// Holds the URL for EDIPost API interface
        /// </summary>
        public string baseurl { get; set; }
        

        /// <summary>
        /// Holds the API key to identify the user
        /// </summary>
        public string apikey { get; set; }
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
        /// Public method to do the GET request
        /// </summary>
        /// <param name="url">The partial url for the request</param>
        /// <param name="data">the data to be posted</param>
        /// <param name="headers">additional headers</param>
        /// <param name="accept">Accepted datataype</param>
        /// <param name="contenttype">The contenttype</param>
        /// <returns>XmlDocument</returns>
        public XmlDocument http_get(string url, XmlDocument data = null, List<String> headers = null, string accept = null, string contenttype = null )
        {
            return _getRequest(url, headers, data, accept, contenttype);
        }



        /// <summary>
        /// Public method to do the POST request
        /// </summary>
        /// <param name="url">The partial url for the request</param>
        /// <param name="data">the data to be posted</param>
        /// <param name="headers">additional headers</param>
        /// <param name="accept">Accepted datataype</param>
        /// <param name="contenttype">The contenttype</param>
        /// <returns>XmlDocument</returns>
        public XmlDocument http_post(string url, XmlDocument data = null, List<String> headers = null, string accept = null, string contenttype = null)
        {
            return _postRequest(url, headers, data, accept, contenttype);
        }




        /// <summary>
        /// private method to handle the POST request
        /// </summary>
        /// <param name="url">The partial url for the request</param>
        /// <param name="data">the data to be posted</param>
        /// <param name="headers">additional headers</param>
        /// <param name="accept">Accepted datataype</param>
        /// <param name="contenttype">The contenttype</param>
        /// <returns>XmlDocument</returns>
        private XmlDocument _postRequest(string url, List<String> headers = null, XmlDocument data = null, string accept = null, string contenttype = null)
        {
            return _handleRequest( url, Constants._post_, headers, data, accept, contenttype );
        }





        /// <summary>
        /// Private method to handle the getRequest
        /// </summary>
        /// <param name="url">The partial url for the request</param>
        /// <param name="data">the data to be posted</param>
        /// <param name="headers">additional headers</param>
        /// <param name="accept">Accepted datataype</param>
        /// <param name="contenttype">The contenttype</param>
        /// <returns>XmlDocument</returns>
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

                    // Workaround to make sure we dont loose empty elements to shorthand structure
                    data_string = data_string.Replace("&amp;nbsp;", " ");


                    MemoryStream sw = new MemoryStream();

                    // Make sure we send data with correct encoding.
                    byte[] buffer = Encoding.UTF8.GetBytes(data_string);
                    
                    req.ContentLength = buffer.Length;

                    Stream PostData = req.GetRequestStream();
                    PostData.Write(buffer, 0, buffer.Length);
                    PostData.Close();
                }


                HttpWebResponse webResp = (HttpWebResponse) req.GetResponse();
                StreamReader response = new StreamReader(webResp.GetResponseStream());
                string response_raw = response.ReadToEnd();

                try
                {
                    xml.LoadXml(response_raw);
                }
                catch (XmlException)
                {
                    // create a xml datawrapper
                    xml = new XmlDocument();
                    XmlDeclaration dec = xml.CreateXmlDeclaration("1.0", null, null);
                    xml.AppendChild(dec);
                    XmlElement root = xml.CreateElement("data");
                    //root.InnerText = System.Convert.ToBase64String(System.Text.Encoding.Unicode.GetBytes(response_raw));
                    root.InnerText = System.Convert.ToBase64String(System.Text.Encoding.Unicode.GetBytes(response_raw));
                    xml.AppendChild(root);   
                }
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError && e.Response != null)
                {
                    var resp = (HttpWebResponse) e.Response;
                    StreamReader response = new StreamReader(resp.GetResponseStream());
                    string responseText = response.ReadToEnd();
                    response.Close();

                    throw new Exceptions.HttpException( resp.StatusDescription + " (" + (int)resp.StatusCode + ") - " + responseText );

                } else
                {
                    throw e;
                }

            }
            catch (Exception e)
            {
                throw new Exceptions.HttpException(e.Message, e);

            }

            return xml;
        }

        internal byte[] http_get_raw(string url, string accept, string contenttype)
        {
            byte[] data;

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


                req.Method = Constants._get_;
                req.KeepAlive = true;


                HttpWebResponse webResp = (HttpWebResponse)req.GetResponse();

                

                using (var memoryStream = new MemoryStream())
                {
                    webResp.GetResponseStream().CopyTo(memoryStream);
                    data = memoryStream.ToArray();
                }


            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError && e.Response != null)
                {
                    var resp = (HttpWebResponse)e.Response;
                    StreamReader response = new StreamReader(resp.GetResponseStream());
                    string responseText = response.ReadToEnd();
                    response.Close();

                    throw new Exceptions.HttpException(resp.StatusDescription + " (" + (int)resp.StatusCode + ") - " + responseText);

                }
                else
                {
                    throw e;
                }

            }
            catch (Exception e)
            {
                throw new Exceptions.HttpException(e.Message, e);
            }

            return data;

        }
    }
}
