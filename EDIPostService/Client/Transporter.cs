﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace EDIPostService.Client
{
    [XmlRoot("transporter")]
    public class Transporter
    {
        #region properties
        [XmlAttribute("id")]
        public int id { get; set; }

        [XmlElement("name")]
        public string name { get; set; }
        #endregion
    }
}