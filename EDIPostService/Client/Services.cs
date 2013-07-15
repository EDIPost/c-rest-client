using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDIPostService.Client
{
    public class Services
    {
        #region properties
        protected List<Service> services = new List<Service>();
        #endregion

        public void addService(Service s){
            this.services.Add(s);
        }

        public List<Service> getServices()
        {
            return this.services;
        }
    }
}
