using SMS.DataService;
using SMS.Enums;
using SMS.IDataService;

using System.Web.Http;

namespace SMS.WebApi.Controllers
{
    public class ApiSendSMSController : ApiController
    {
        ISMS sms;
        public ApiSendSMSController()
        {
            sms = GetSMSProvider();
        }

        private ISMS GetSMSProvider()
        {
            if (Entities.ApplicationSetting.SMSProvider == SMSProvider.VictoryLink)
            {
                return new VictoryLinkDSL();
            }
            else if (Entities.ApplicationSetting.SMSProvider == SMSProvider.AsiaCell)
            {
                return new AsiaCellDSL();
            }
            else if (Entities.ApplicationSetting.SMSProvider == SMSProvider.SMSMisr)
            {
                return new SMSMisrDSL();
            }
            else if (Entities.ApplicationSetting.SMSProvider == SMSProvider.Vodafone)
            {
                return new VodafoneSMSDSL();
            }
            else if (Entities.ApplicationSetting.SMSProvider == SMSProvider.Sinch)
            {
                return new SinchDSL();
            }
            else if (Entities.ApplicationSetting.SMSProvider == SMSProvider.SmartSmsGateWay)
            {
                return new SmartSmsGateway();
            }
            else if (Entities.ApplicationSetting.SMSProvider == SMSProvider.Mshastra)
            {
                return new MshastraDSL();
            }
            else if (Entities.ApplicationSetting.SMSProvider == SMSProvider.SLTsrilanka)
            {
                return new SLTsrilankaDSL();
            }
            else if (Entities.ApplicationSetting.SMSProvider == SMSProvider.GlobalSms)
            {
                return new GlobalSmsDSL();
            }
            else if (Entities.ApplicationSetting.SMSProvider == SMSProvider.SmartMessagingEtisalat)
            {
                return new SmartMessagingEtisalatDSL();
            }
            else if (Entities.ApplicationSetting.SMSProvider == SMSProvider.NdmSolutions)
            {
                return new NdmSolutionsDSL();
            }
            else if (Entities.ApplicationSetting.SMSProvider == SMSProvider.Aptivadigi)
            {
                return new AptivadigiDSL();
            }
            else if (Entities.ApplicationSetting.SMSProvider == SMSProvider.Alibaba)
            {
                return new AlibabaDSL();
            }
            else if (Entities.ApplicationSetting.SMSProvider == SMSProvider.GlobalSMS_EDS_UAE)
            {
                return new GlobalSMS_EDS_UAEDSL();
            }
            else if (Entities.ApplicationSetting.SMSProvider == SMSProvider.RouteMobile)
            {
                return new RouteMobileDSL();
            }
            else if (Entities.ApplicationSetting.SMSProvider == SMSProvider.Elitbuzz)
            {
                return new ElitbuzzDSL();
            }
            else if (Entities.ApplicationSetting.SMSProvider == SMSProvider.MIM)
            {
                return new MIMDSL();
            }
            else if (Entities.ApplicationSetting.SMSProvider == SMSProvider.Ebulk)
            {
                return new EbulkDSL();
            }
            else if (Entities.ApplicationSetting.SMSProvider == SMSProvider.Mora)
            {
                return new MoraDSL();
            }
            else if (Entities.ApplicationSetting.SMSProvider == SMSProvider.VictoryLinkV2)
            {
                return new VictoryLinkV2DSL();
            }
            else if (Entities.ApplicationSetting.SMSProvider == SMSProvider.MoraV2)
            {
                return new MoraV2DSL();
            }
            else if (Entities.ApplicationSetting.SMSProvider == SMSProvider.DeliverSmart)
            {
                return new SmsDeliverSmartDSL();
            }
            else if (Entities.ApplicationSetting.SMSProvider == SMSProvider.Brcitco)
            {
                return new BrcitcoDSL();
            }
            else if (Entities.ApplicationSetting.SMSProvider == SMSProvider.SMSMisrV2)
            {
                return new SMSMisrV2DSL();
            }
            else if (Entities.ApplicationSetting.SMSProvider == SMSProvider.CommunityAdsDSL)
            {
                return new CommunityAdsDSL();
            }
            else if (Entities.ApplicationSetting.SMSProvider == SMSProvider.MersalSMS)
            {
                return new MersalSMSDSL();
            }
            else if (Entities.ApplicationSetting.SMSProvider == SMSProvider.Adafsa)
            {
                return new Adafsa();
            }
                return null;
        }

        [HttpGet]
        public string SendSMS(string mobileNumber, string message)
        {
            return sms.SendSMS(mobileNumber, message);
        }
    }
}
