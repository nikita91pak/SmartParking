using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiUserSmartParcking.Model
{
    class Enteres
    {
       
        private Driver driver;
        public Driver Driver
        {
            get { return this.driver; }
            set { this.driver = value; }
        }

        private  DateTime enter;
        public   DateTime Enter
        {
            get { return this.enter; }
            set { this.enter = value; }
        }

       private DateTime exit;
        public DateTime Exit
        {
            get { return this.exit; }
            set { this.exit = value; }
        }

        
    }
}
