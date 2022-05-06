using Newtonsoft.Json;
using NLog;
using PhoneValidationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace PhoneValidationService.Controllers
{
    public class PhoneController : ApiController
    {

        // string BaseUrl = "http://ADVISED-IP/nibss/phone/validation/";
        private string Baseurl = System.Configuration.ConfigurationManager.AppSettings["Baseurl"];
        private static Logger logger = LogManager.GetCurrentClassLogger();


        [HttpPost]
        [Route("api/Phone/PostPhoneNum")]
        public async Task<object> PostPhoneNo(PhoNo ph)
        {
            object PhNoInfo = null;
            PhoNo orb = ph;
            PhoNo sts = new PhoNo();
            ErrorStatus err = null;
            Boolean status = true;

            if (String.IsNullOrEmpty(orb.phone))
            {
                err.Statuscode = "01";
                err.message = "Empty Request";
                err.status = false;
            }
            else
            {
                status = true;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage hrm = await client.PostAsJsonAsync("/search", ph);
                    var phResponse = hrm.Content.ReadAsStringAsync().Result;

                    try
                    {
                        logger.Info("Response " + "Code:00" + "message: Successful" + phResponse + Environment.NewLine + DateTime.Now);
                        if (hrm.IsSuccessStatusCode)
                        {
                            PhNoInfo = JsonConvert.DeserializeObject<object>(phResponse);
                        }
                        else if (hrm.StatusCode == HttpStatusCode.BadRequest)
                        {
                            err = JsonConvert.DeserializeObject<ErrorStatus>(phResponse);
                            sts.err.status = false;
                        }
                        else if (hrm.StatusCode == HttpStatusCode.InternalServerError)
                        {
                            err = JsonConvert.DeserializeObject<ErrorStatus>(phResponse);
                            sts.err.status = false;
                        }
                        else if (hrm.StatusCode == HttpStatusCode.NotImplemented)
                        {
                            err = JsonConvert.DeserializeObject<ErrorStatus>(phResponse);
                            sts.err.status = false;
                        }
                        else if (hrm.StatusCode == HttpStatusCode.NotFound)
                        {
                            err = JsonConvert.DeserializeObject<ErrorStatus>(phResponse);
                            sts.err.status = false;
                        }
                        else if (hrm.StatusCode == HttpStatusCode.Forbidden)
                        {
                            err = JsonConvert.DeserializeObject<ErrorStatus>(phResponse);
                            sts.err.status = false;
                        }
                        else if (hrm.StatusCode == HttpStatusCode.BadGateway)
                        {
                            err = JsonConvert.DeserializeObject<ErrorStatus>(phResponse);
                            sts.err.status = false;
                        }
                        else if (hrm.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            err = JsonConvert.DeserializeObject<ErrorStatus>(phResponse);
                            sts.err.status = false;

                        }
                    }
                    catch (Exception ex)
                    {
                        PhNoInfo = ex;
                        sts.err.message = "Failed :02";
                        logger.Error(ex, "code:02 message:Failed", ex);
                    }
                    if (err != null) return err;
                    else
                        return PhNoInfo;
                }
            }
            if (status) return PhNoInfo;
            else return err;

        }

        

    }
}
