using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace thewall9.web.parent.BLL
{
    public class BaseBLL
    {
        private WebClient MyWebClient
        {
            get
            {
                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;
                return client;
            }
        }
        protected T DownloadObject<T>(string URI)
        {
            try
            {
                using (var _c = MyWebClient)
                {
                    var _TimeElapsed = DateTime.Now;
                    var _Page = JsonConvert.DeserializeObject<T>(_c.DownloadString(APP._API_URL + URI));
                    var _Output="Finish Web Request -> " + APP._API_URL + URI + " TIME: " + DateTime.Now.Subtract(_TimeElapsed).Milliseconds + " Milliseconds";
                    //System.Diagnostics.Debug.WriteLine(_Output, "thewall9");
                    System.Diagnostics.Trace.WriteLine(_Output, "thewall9");
                    return _Page;
                }
            }
            catch (System.Net.WebException e)
            {
                if ((((HttpWebResponse)(e.Response)).StatusCode).Equals(HttpStatusCode.BadRequest))
                {
                    return default(T);
                }
                throw new System.Net.WebException("Error Calling: " + URI, e.InnerException);
            }
        }
    }
}
