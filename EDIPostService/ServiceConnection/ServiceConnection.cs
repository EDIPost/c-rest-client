using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

       
    }
}
