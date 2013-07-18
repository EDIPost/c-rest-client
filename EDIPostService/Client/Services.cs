using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace EDIPostService.Client
{
    /// <summary>
    /// A container class for multiple items
    /// </summary>
    public class Services : List<Service>{}
}
