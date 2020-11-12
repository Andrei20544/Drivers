using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Zadanie1.Interface;

namespace Zadanie1
{
    class DriverViewModel : INotifyPropertyChanged
    {
        private Drivers selectedDriver { get; set; }

        public ObservableCollection<Drivers> Driverss { get; set; }

        public Drivers SelectedDriver
        {
            get { return selectedDriver; }
            set
            {
                selectedDriver = value;
                OnPropertyChanged("SelectedDriver");
            }
        }

        public void ClearSelected()
        {
            SelectedDriver = null;
        }

        private Command addCommand;
        private Command saveCommand;
        private Command removeCommand;

        //AddCommand
        public Command AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new Command(obj =>
                    {
                        using (ModelDB md = new ModelDB())
                        {
                            Drivers driver = new Drivers();
                            Driverss.Insert(0, driver);
                            md.Drivers.Add(driver);
                            md.SaveChanges();
                            SelectedDriver = driver;
                        }
                    }));
            }
        }
        //RemoveCommand
        public Command RemoveCommand
        {
            get
            {
                return removeCommand ??
                    (removeCommand = new Command(obj =>
                    {
                        Drivers driver = obj as Drivers;
                        if (driver != null)
                        {
                            using (ModelDB mb = new ModelDB())
                            {
                                List<Drivers> temp = mb.Drivers.ToList();
                                var selecteditem = from t in mb.Drivers
                                                   where driver.ID == t.ID
                                                   select t;
                                mb.Drivers.Remove(selecteditem.FirstOrDefault());

                                mb.SaveChanges();
                            }
                            Driverss.Remove(driver);
                        }
                    },
                    (obj) => Driverss.Count > 0));
            }
        }
        //SaveCommand
        public Command SaveCommand
        {
            get
            {
                return saveCommand ??
                    (saveCommand=new Command(obj=>
                    {
                        using (ModelDB md = new ModelDB())
                        {
                            md.Entry(SelectedDriver).State = System.Data.Entity.EntityState.Modified;
                            md.SaveChanges();
                        }
                    }));
            }
        }

        public string Name { get { return SelectedDriver.Name; } set { SelectedDriver.Name = value; OnPropertyChanged("Name"); } }
        public string MiddleName { get { return SelectedDriver.MiddleName; } set { SelectedDriver.MiddleName = value; OnPropertyChanged("MiddleName"); } }
        public string Photo { get { return SelectedDriver.Photo; } set { SelectedDriver.Photo = value; OnPropertyChanged("Photo"); } }

        public void Update()
        {
            using (ModelDB md = new ModelDB())
            {
                md.Entry(SelectedDriver).State = System.Data.Entity.EntityState.Modified;
                md.SaveChanges();
            }
        }

        private int Con = 0;

        public DriverViewModel()
        {
            Driverss = new ObservableCollection<Drivers>();
            using (ModelDB md = new ModelDB())
            {
                var licences = md.Licences.ToList();
                var temp = md.Drivers.ToList();

                foreach (var item in temp)
                {
                    if (licences[item.ID - 1].Name == item.Name &&
                        licences[item.ID - 1].ExpireDate.Value.Year == licences[item.ID - 1].LicenceDate.Value.Year) item.Status = "Изъят";
                    else if (licences[item.ID - 1].Name == item.Name && licences[item.ID - 1].ExpireDate < DateTime.Now && 
                        licences[item.ID - 1].LicenceDate.Value.Year != licences[item.ID - 1].ExpireDate.Value.Year) item.Status = "Утратил силу";
                    else item.Status = "Активен";

                    item.Photo = $@"{AppDomain.CurrentDomain.BaseDirectory}/Assets/" + item.Photo;
                    Driverss.Add(item);
                    CountObject();
                }
            }

        }

        public int CountObject()
        {
            Con++;
            return Con;
        }

        public DriverViewModel(List<Drivers> list)
        {

            Driverss = new ObservableCollection<Drivers>();
            Driverss.Clear();

            int count = list.Count;

            for (int i = 0; i < count; i++)
            {
                Driverss.Add(list[i]);
            }

        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
