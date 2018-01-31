using GuiUserSmartParcking.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json.Linq;

namespace GuiUserSmartParcking.Model
{
    class Driver
    {
       public static Dictionary<string, Driver> drivers = new Dictionary<string, Driver>();
      
        private string plateNumber;
        public  string PlateNumber
        {
            get { return this.plateNumber; }
            set { this.plateNumber = value; }
        }

        private string idDriver;
        public  string IdDriver
        {
            get { return this.idDriver; }
            set { this.idDriver = value; }
        }

        private string lastName;
        public string LastName
        {
            get { return this.lastName; }
            set { this.lastName = value; }
        }

        private string firstName;
        public string FirstName
        {
            get { return this.firstName; }
            set { this.firstName = value; }
        }

        private Status status;
        public Status Status
        {
            get { return this.status; }
            set { this.status = value; }
        }
        

        public Driver()
        {

        }

        public static void cashData()
        {
            Json json = new Json(@"https://alpradministration.000webhostapp.com/");// rest api from server
            json.ParseRequestGet(); // Parse to JSON
            string jsonString = string.Empty; 

            foreach (JObject obj in json.JArr.Children<JObject>())
            {
                Driver d = new Driver(); // set new drivers from json
                d.status = new Status("1000", "Aproval");
                foreach (JProperty p in obj.Properties())// loop - save a data from JSON
                {

                    switch (p.Name)
                    {
                        case "0":
                            d.PlateNumber = (string)p.Value;
                            continue;
                        case "3":
                            d.IdDriver = (string)p.Value;
                            continue;
                        case "1":
                            d.FirstName = (string)p.Value;
                            continue;
                        case "2":
                            d.LastName = (string)p.Value;
                            continue;
                    }

                }
                drivers.Add(d.PlateNumber, d); // save in the hash map
            }
        }

        public static void DictionaryAdd(string key,Driver value)
        {
            drivers.Add(key, value);
        }

        public static Driver getDriver(string key)
        {
            Driver d = new Driver();
            key = FormatKey(key);
            return drivers.ContainsKey(key) ? drivers[key] : null;
        }

        public static string FormatKey(string str)
        {
            int index = 0;
            int lenght = str.Length;

            do
            {
                if (str[index] == '-')
                {
                    str = str.Remove(index, 1);
                    lenght = str.Length;
                    index++;
                }
                else if (str[index] >= '0' && str[index] <= '9')
                {
                    index++;

                }
                else
                {
                    return "Error";
                }
            }
            while (index < lenght);

            return str;
        }

    }
}
