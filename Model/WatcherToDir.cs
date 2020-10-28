using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using GuiUserSmartParcking.Data;
using System.Media;
using System.Windows.Controls;

namespace GuiUserSmartParcking.Model
{
    class WatcherToDir
    {
        private FileSystemWatcher watcher;
        public FileSystemWatcher Watcher
        {
            get { return this.watcher; }
            set { this.watcher = value; }
        }
        private Proccess proc;
        public Proccess Process
        {
            get { return this.proc; }
            set { this.proc = value; }
        }

        private string path = string.Empty;

        public WatcherToDir()
        {
            this.watcher = new FileSystemWatcher();
            this.proc = new Proccess();
        }

        

        public void watch()
        {
            this.watcher.Path = @"C:\Users\Inna\Documents\img";
            this.watcher.NotifyFilter = NotifyFilters.LastWrite;
            this.watcher.Filter = "*";
            this.watcher.EnableRaisingEvents = true;
        }
       

        public void ClearDir()
        {

            DirectoryInfo di = new DirectoryInfo(@"C:\Users\Inna\Documents\img");
           
            foreach(FileInfo files in di.GetFiles())
            {
                files.Delete();
            }
            
        }
    }
    
}
