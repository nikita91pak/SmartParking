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
        //var
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
        

        public Driver()//constuctor
        {

        }

        public static void cashData()//this function are get all data form server
        {
            Json json = new Json(@"https://api.mongolab.com/api/1/databases/cars/collections/drivers?apiKey=_IUolN87EnEDzGqlWEQ6pA2fXkp-IZdA");// rest api from server
            json.ParseRequestGet(); // Parse to JSON
            string jsonString = string.Empty; 

            foreach (JObject obj in json.JArr.Children<JObject>())
            {
                Driver d = new Driver(); // set new drivers from json
                d.status = new Status();
                foreach (JProperty p in obj.Properties())// loop - save a data from JSON 
                {

                    switch (p.Name)
                    {
                        case "first_name":
                            d.FirstName = (string)p.Value;
                            continue;
                        case "last_name":
                            d.LastName = (string)p.Value;
                            continue;
                        case "id":
                            d.IdDriver = (string)p.Value;
                            continue;
                        case "p_num":
                            d.PlateNumber = (string)p.Value;
                            continue;
                    }
                  
                }
                d.status.IdStatus = "1000";
                d.status.NameStatus = "Allowed";
                drivers.Add(d.PlateNumber, d); // save in the hash map
            }
        }

        public static void DictionaryAdd(string key,Driver value)// add new driver to Dictionary (hash map the key is plate number)
        {
            drivers.Add(key, value);
        }

        public static Driver getDriver(string key)//get curetly driver form Dictionary
        {
            Driver d = new Driver();
            key = FormatKey(key);
            return drivers.ContainsKey(key) ? drivers[key] : null;// retuent result if the plate is exist
        }

        public static string FormatKey(string str)//this transform from simple plate number to key in Dictionary
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
