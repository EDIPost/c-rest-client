using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using EPService = EDIPostService.ServiceConnection;

namespace EDIPostServiceTests.ServiceConnection
{
    public class ServiceConnection
    {
        [Fact]
        public void ConstructorTest()
        {
            EPService.ServiceConnection sc = new EPService.ServiceConnection("http://api.edipost.no", "12312-1231321-12312312");

            Assert.Equal("http://api.edipost.no", sc.baseurl);
        }
    }
}
