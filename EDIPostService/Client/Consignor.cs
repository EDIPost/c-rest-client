﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EDIPostService.Client
{
    /// <summary>
    /// The consignor class.
    /// </summary>
    [XmlRoot("consignor")]
    public class Consignor : Party
    {
       /* [XmlAttribute("id")]
        public int id { get; set; }*/
    }
}
