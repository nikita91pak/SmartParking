using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace GuiUserSmartParcking.Model
{
    public enum HttpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    class RestApi
    {
        private string endPoint;
        public string EndPoint
        {
            get { return this.endPoint; }
            set { this.endPoint = value; }
        }

        private string strResponseValue;
        public string StrResponsValue
        {
            get { return this.strResponseValue; }
            set { this.strResponseValue = value; }
        }

        private HttpVerb httpMethod;
        public HttpVerb HttpMethod
        {
            get { return this.httpMethod; }
            set { this.httpMethod = value; }
        }

        public RestApi()
        {
            this.endPoint = string.Empty;
          
        }

        public void MakeReguestGet()
        {
            this.httpMethod = HttpVerb.GET;
            this.strResponseValue = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);
            request.Method = httpMethod.ToString();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ApplicationException("Error code: " + response.StatusCode);
            }
            else
            {
                Stream responseStream = response.GetResponseStream();
                if (responseStream != null)
                {
                    StreamReader reader = new StreamReader(responseStream);
                    this.strResponseValue = reader.ReadToEnd();
                    reader.Close();
                }
            }
            
        }

    }
}
