
using System.Xml.Serialization;

namespace SMS.Entities.AsiaCell
{
    [XmlRoot("AuthResult")]
    public class AuthResultDTO
    {
        public string Result { get; set; }
    }
}
