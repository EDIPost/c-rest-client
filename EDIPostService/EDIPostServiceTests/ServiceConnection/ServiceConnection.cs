using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Xunit;
using EPService = EDIPostService.ServiceConnection;

namespace EDIPostServiceTests.ServiceConnection
{
    public class ServiceConnection
    {
        [Fact]
        public void ConstructorTest()
        {
            EPService.ServiceConnection sc = new EPService.ServiceConnection("http://apitest.edipost.no", "12312-1231321-12312312");

            Assert.Equal("http://apitest.edipost.no", sc.baseurl);
        }

        [Fact]
        public void ConstructorTest2()
        {
            EPService.ServiceConnection sc = new EPService.ServiceConnection("http://apitest.edipost.no", "12312-1231321-12312312");

            Assert.Equal("12312-1231321-12312312", sc.apikey);
        }

        [Fact]
        public void GetRequestTest() 
        {
            XmlDocument result;

            EPService.ServiceConnection sc = new EPService.ServiceConnection("http://apitest.edipost.no", "bfa6b907ab69a8bdc2a3661cb6b20d7464bed04b");
            result = sc.http_get("/");
            XmlNodeList xlist = result.SelectNodes("/collection/links");

            Assert.True(xlist.Count > 0);
        }

    }
}
