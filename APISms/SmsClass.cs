using APISms.Need;
using KashanPayam;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace APISms
{
    public class SmsClass
    {
        AppSettingClass appSettingClass;
        string userName = "";
        string password = "";
        string senderNumber = "";
        public SmsClass()
        {
            appSettingClass = Need.Codes.GetSmsProperties();
            userName = appSettingClass.smsProperties.Username;
            password = appSettingClass.smsProperties.Password;
            senderNumber = appSettingClass.smsProperties.Number;
        }

        public async Task<int> SendAsync(string body, string[] recipientNumber)
        {
            DatabaseLayer.Access.LogSmsOp logSmsOp = new DatabaseLayer.Access.LogSmsOp();
            string numbers = "";
            string recs = "";

            try
            {
                
                var serviceSms = new KashanPayam.SendSoapClient(SendSoapClient.EndpointConfiguration.SendSoap);
                var arrayNumber = new ArrayOfString();
                var arrayRecId = new ArrayOfLong();
                foreach (var str in recipientNumber)
                {
                    arrayNumber.Add(str);
                    DateTime dt = Codes.GetLocalDateTime();
                    long recid = dt.Ticks;
                    arrayRecId.Add(recid);
                    numbers += str + ",";
                    recs += Convert.ToString(recid) + ",";
                }
                SendSmsResponse sendSmsResponse = await serviceSms.SendSmsAsync(userName, password, arrayNumber, senderNumber, body, false, "", arrayRecId, null);

                numbers = numbers.EndsWith(",") ? numbers.Substring(0, numbers.LastIndexOf(",")) : numbers;
                recs = recs.EndsWith(",") ? recs.Substring(0, recs.LastIndexOf(",")) : recs;

                DatabaseLayer.Models.LogSms logSms = new DatabaseLayer.Models.LogSms();
                logSms.CreateDate = Codes.GetLocalDateTime();
                logSms.MessageText = body;
                logSms.SendNumber = senderNumber;
                logSms.MobileNumbers = numbers;
                logSms.RecId = recs;
                logSms.LogSmsResultId = sendSmsResponse.Body.SendSmsResult;              
                await logSmsOp.InsertLogSms(logSms);

                return sendSmsResponse.Body.SendSmsResult;           
            }
            catch (Exception ex)
            {
                DatabaseLayer.Models.LogSms logSms = new DatabaseLayer.Models.LogSms();
                logSms.CreateDate = Codes.GetLocalDateTime();
                logSms.MessageText = body;
                logSms.SendNumber = senderNumber;
                logSms.MobileNumbers = numbers;
                logSms.RecId = recs;
                logSms.LogSmsResultId = -1;
                logSms.Explain = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                await logSmsOp.InsertLogSms(logSms);

                return -1;
            }
        }
    }
}
