
using GuiUserSmartParcking.Data;
using GuiUserSmartParcking.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
 

namespace GuiUserSmartParcking
{
    /// <summary>
    /// Interaction logic for LPR.xaml
    /// </summary>
    public partial class LPR : Window
    {
        private WatcherToDir watcher; //This class is responsible to lisen for a new files in dir the  path of dir is D:\img
        private List<Enteres> enteres; // this class mangen all enteres and exits of all cars in gate.
 
        /// <summary>
        /// In this Constructor sets variables watcher and enteres and added event for lisen  to dir.
        /// </summary>
        public LPR()
        {
            InitializeComponent();
            watcher = new WatcherToDir();
            enteres = new List<Enteres>();
            Loaded += LPR_Loaded;  
        }

        /// <summary>
        /// Event loaded, 
        /// method wacth from class watcher intended for set wich of dir applction going to lisen.
        /// and program add event onChange for starting lisen a choosen dir.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void LPR_Loaded(object sender, RoutedEventArgs e)
        {
            this.watcher.watch();
            this.watcher.Watcher.Changed += new FileSystemEventHandler(OnChanged);
            Driver.cashData();    
        }

        /// <summary>
        ///In event onChange program calling to proccess alpr -c ue for procces a picture and return result. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            lock (this.watcher.Process)
            {
                DirectoryInfo diProc = new DirectoryInfo(@"D:\img");//get all file from dir
                FileInfo[] file = diProc.GetFiles(); //cash all file
                if (file.Length - 1 != -1) // this condition check if the dir is not empty.
                {
                    Random nameFile = new Random(); //  giving a random name for a file jpg
                    this.watcher.Process.Comand = "alpr -c eu " + file[0].FullName; // set comand and path of picture
                    this.watcher.Process.CreateProcess();//set Proccess 
                    this.watcher.Process.Run(); // start to process a pictrue and rerun result.
                    FileInfo[] files = diProc.GetFiles(); // cash file
                    File.Copy(files[0].FullName, @"D:\bitmapImage\" + nameFile.Next(1, 500) + ".jpg", true); //copy to onther dir for bitmap a picture.
                    this.Dispatcher.Invoke(() => this.TB_IsEqvauls.Text =  FormatPlate(this.watcher.Process.Result));// set text box for call event ChangeText
                    this.Dispatcher.Invoke(() => this.TB_IsEqvauls.Text = string.Empty);
                    this.watcher.ClearDir(); // clear dir.

                }
            }
        }

        /// <summary>
        /// this event added to text box TB_IsEqvauls here applaction checking for a existed a plate number in data driver
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TB_IsEqvauls_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TB_IsEqvauls.Text))// this condition check if text box Name TB_IsEqvauls is not empty or null.
            {
                string key = this.TB_IsEqvauls.Text; // set a key for searching exist in Dictionary 
                Enteres item = new Enteres();// create a new entery car
                item.Driver = Driver.getDriver(key); // cheking if this plate number is exist in Dictionary .
                FillDetails(item.Driver); //fill all fialds with deitels driver if the plate number is exist data if not Exist all fialds are stay empty.
                sourceImage(); // 
                ControlEnteres(item);
            }
        }

        /// <summary>
        /// this method is for fill dietels about a driver if he exist in data.
        /// </summary>
        /// <param name="d">this Param is from class Driver </param>
        private void FillDetails(Driver d)
        {    
            if(d != null)
            {
                this.TB_id.Text = d.IdDriver;
                this.TB_FirstName.Text = d.FirstName;
                this.TB_LastName.Text = d.LastName;
                this.TB_PlateNumber.Text =FormatPlate(d.PlateNumber);
            }
            else
            {
                this.TB_id.Text = string.Empty;
                this.TB_FirstName.Text = string.Empty;
                this.TB_LastName.Text = string.Empty;
               FormatPlate(watcher.Process.Result);
                setStatus("Error",null);
            }
        }

        /// <summary>
        /// this method set image  cars in gate, in real time.
        /// </summary>
        private void sourceImage()
        {
            DirectoryInfo diImage = new DirectoryInfo(@"D:\bitmapImage");
            FileInfo[] files = diImage.GetFiles();
            diImage.Refresh();
            this.Img_NumPlate.Source = new BitmapImage(new Uri(files[0].FullName));
        }

        /// <summary>
        /// This method return a status for a user.
        /// </summary>
        /// <param name="status"></param>
        /// <param name="e"></param>
        private void setStatus(string status,Enteres e)
        {
           switch(status)
            {
                case "Enter":
                    this.TBLOCK_Status.Text = string.Format("Hello Ms/M {0} {1} Have a nice day", e.Driver.FirstName, e.Driver.LastName);
                    break;
                case "Exit":
                    this.TBLOCK_Status.Text = string.Format("GoodBay Ms/M {0} {1} Have a nice day see you tommorow", e.Driver.FirstName, e.Driver.LastName);
                    break;
                default:
                    this.TBLOCK_Status.Text = "Not Fimilatry Car";
                    break;
            }
        }

        /// <summary>
        /// Method convert to standard israeli format plate number.  
        /// </summary>
        /// <param name="plate">Type of var is string </param>
        /// <returns></returns>
        private string FormatPlate(string plate)
        {
           plate = plate.Insert(2, "-");
           plate = plate.Insert(6, "-");
           return plate;
        }

        /// <summary>
        /// When car is first time in gate or a new enter method set enter=date today  and exit = null while car exit applction set exit today too. 
        /// </summary>
        /// <param name="e">From class Enteres </param>
        private void ControlEnteres(Enteres e)
        {
            
            int index = 0;
            if(e.Driver != null)
                {
                 List<int> saveIndexs = new List<int>();
                   string plate = e.Driver.PlateNumber;

                      if (this.enteres.Count != 0)
                      {
                          foreach (Enteres ent in this.enteres)
                              {
                                if (ent.Driver.PlateNumber.Equals(plate))
                                  saveIndexs.Add(index);
                                    index++;
                          }

                            if (saveIndexs.Count == 0)
                             {
                               e = Enter_Exit(e);
                                this.enteres.Add(e);
                            }
                            else
                            {
                                 foreach (int i in saveIndexs)
                                     this.enteres[i] = Enter_Exit(this.enteres[i]);
                            }
                      }

                     else
                     {
                        e = Enter_Exit(e);
                         this.enteres.Add(e);
                     }
            }
            
               
        }

        private Enteres Enter_Exit(Enteres item)
        {
            Random random = new Random();
            Dal dal = new Dal();
            string query;
            if ((item.Enter.Year == 1 && item.Exit.Year == 1) || item.Enter.Year != 1 && item.Exit.Year != 1)
            {
                item.Enter = DateTime.Now;
                item.Exit = new DateTime();
                setStatus("Enter", item);
                query = string.Format("INSERT INTO Enteres VALUES ({0}, {1}, {2}, '{3}', '{4}', '{5}')", random.Next(1, 1000), int.Parse(item.Driver.Status.IdStatus), 
                    int.Parse(item.Driver.PlateNumber), item.Driver.Status.NameStatus, item.Enter, null);
                dal.DeleteInsertUpdate(query);
                this.DG_History.ItemsSource = dal.GetDataTable("SELECT e.*, d.*   FROM Enteres as e INNER JOIN Drivers as d on e.p_num = d.p_num").DefaultView;
            }
            else if (item.Enter.Year != 1 && item.Exit.Year == 1)
            {
                item.Exit = DateTime.Now;
                setStatus("Exit", item);
            }
            return item;
        }
    }
}
