using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace EDIPostService.Client
{

    /// <summary>
    /// The transporter/Carrier class
    /// </summary>
    [XmlRoot("transporter")]
    public class Transporter
    {
        #region properties
        /// <summary>
        /// The id of the transporter
        /// </summary>
        [XmlAttribute("id")]
        public int id { get; set; }


        /// <summary>
        /// the name of the transporter
        /// </summary>
        [XmlElement("name")]
        public string name { get; set; }
        #endregion
    }
}
