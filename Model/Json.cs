using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiUserSmartParcking.Model
{
    class Json
    {
        private RestApi clientRest = new RestApi();

        private string restValue;
        public string RestValue
        {
            get { return this.restValue; }
            set { this.restValue = value; }
        }

        private JArray jarray;
        public JArray JArr
        {
            get { return this.jarray; }
            set { this.jarray = value; }
        }

        public Json(string Url)
        {
            this.clientRest.EndPoint = Url;
        }

        public void ParseRequestGet()
        {
             this.restValue = string.Empty;
             this.clientRest.MakeReguestGet();
             this.restValue = this.clientRest.StrResponsValue;
             string res = result(RestValue);
             this.jarray = JArray.Parse(res);
        }

        string result(string s)
        {
            string str = string.Empty;

            int i = 0;
            do
            {
                str += s[i];
                i++;
            } while (s[i] != ']');
            str += "]";
            return str;
        }
    }
}
