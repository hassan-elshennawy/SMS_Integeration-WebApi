
using System.Xml.Serialization;

namespace SMS.Entities.AsiaCell
{
    [XmlRoot("SendResult")]
    public class SendResultDTO
    {
        public string Result { get; set; }
    }
}
