using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace GuiUserSmartParcking.Data
{
    class Proccess
    {
        private string comand;
        public string Comand
        {
            get { return this.comand; }
            set { this.comand = value; }
        }

        private string result;
        public string Result
        {
            get { return this.result; }

        }


        private ProcessStartInfo processStarInfo;
        private Process process;

        public Proccess()
        {
            this.processStarInfo = new ProcessStartInfo();
            this.process = new Process();
        }

        public void CreateProcess()
        {

            this.processStarInfo.WorkingDirectory = @"C:\Users\Inna\Downloads\openalpr-2.1.0-win-64bit\openalpr_64";
            this.processStarInfo.FileName = @"C:\Windows\System32\cmd.exe";
            this.processStarInfo.Arguments = "/c" + this.comand;
            this.process.StartInfo = processStarInfo;
            this.process.StartInfo.UseShellExecute = false;
            this.process.StartInfo.RedirectStandardOutput = true;
        }

        public void Run()
        {
            string line = string.Empty;
            this.process.Start();
            line = process.StandardOutput.ReadLine();
            line = process.StandardOutput.ReadLine();
            this.process.WaitForExit();
            if (!string.IsNullOrEmpty(line))
                GetResult(line);
            else
                this.result = string.Empty;
        }

        private void GetResult(string line)
        {
            string result = string.Empty;
            int index = 0;
           
            
            do
            {
                if (line[index] >= '0' && line[index] <= '9')
                {
                    result += line[index];
                    index++;
                }

               else if (line[index] >= 'A' && line[index] <= 'Z')
                {
                    result += CheckNumber(line[index]);
                    index++;  
                }
               else if (line[index] >= 'a' && line[index] <= 'z')
                {
                    result += CheckNumber(line[index]);
                    index++;
                }

                else
                {
                    index++;
                }
            }
            while (line[index] != '\t');
            this.result = result;
        }

        private char CheckNumber(char letter)
        {
            switch (letter)
            {
                case 'B':
                    letter = '8';
                    return letter;
                case 'b':
                    letter = '6';
                    return letter;
                case 'l':
                    letter = '1';
                    return letter;
                case 'O':
                    letter = '0';
                    return letter;
                default:
                    return letter;
            }
        }
    }
}
