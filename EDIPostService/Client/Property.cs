using System.Xml.Serialization;

namespace EDIPostService.Client
{
    /// <summary>
    /// Additional properties to the service.
    /// </summary>
    [XmlRoot("property")]
    public class Property
    {
        /// <summary>
        /// Construct new property
        /// </summary>
        public Property()
        {
        }

        /// <summary>
        /// Construct new property
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public Property(string key, string value)
        {
            this.key = key;
            this.value = value;
        }

        # region properties

        /// <summary>
        /// The name of the service
        /// </summary>
        [XmlAttribute("key")]
        public string key { get; set; }

        /// <summary>
        /// The name of the service
        /// </summary>
        [XmlAttribute("value")]
        public string value { get; set; }

        # endregion
    }
}