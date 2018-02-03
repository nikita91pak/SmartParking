using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiUserSmartParcking.Model
{
    class Status
    {
        private string idStatus;
        public string IdStatus
        {
            get { return this.idStatus; }
            set { this.idStatus = value; }
        }

        private string nameStatus;
        public string NameStatus
        {
            get { return this.nameStatus; }
            set { this.nameStatus = value; }
        }


        public Status()
        {
           
        }
    }
}
