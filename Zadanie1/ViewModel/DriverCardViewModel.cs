using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zadanie1.Interface;
using Zadanie1.Model;

namespace Zadanie1.ViewModel
{
    class DriverCardViewModel : INotifyPropertyChanged
    {
        private DriversAndLicences selectedLicences { get; set; }
        public ObservableCollection<DriversAndLicences> Licencess { get; set; }
        public ObservableCollection<Licences> Licencess_ { get; set; }
        public DriversAndLicences SelectedLicences
        {
            get { return selectedLicences; }
            set
            {
                selectedLicences = value;
                OnPropertyChanged("SelectedLicences");
            }
        }

        public void GetMessage(string msg)
        {
            MessageBox.Show(msg);
        }

        private Command addCommand;
        public Command AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new Command(obj =>
                    {
                        try
                        {
                            using (ModelDB md = new ModelDB())
                            {
                                DriversAndLicences driversAndLicences = new DriversAndLicences();
                                Licences licences = new Licences();
                                Licencess.Insert(0, driversAndLicences);
                                md.Licences.Add(licences);
                                //md.SaveChanges();
                                SelectedLicences = driversAndLicences;
                            }
                        }
                        catch(Exception ex)
                        {
                            GetMessage(ex.Message);
                        }
                    }));

            }
        }

        private Command saveCommand;
        public Command SaveCommand
        {
            get
            {
                return saveCommand ??
                    (saveCommand = new Command(obj =>
                    {
                        using (ModelDB md = new ModelDB())
                        {
                            try
                            {
                                var Lic = from l in Licencess
                                          where l.Name == SelectedLicences.Name
                                          select new
                                          {
                                              Name = l.name,
                                              LicenceDate = l.DateOfIssure,
                                              ExpireDate = l.ExpireDate,
                                              Categories = l.Categories,
                                              LicenceSeries = l.LicenceSeries,
                                              LicNumber = l.Number,
                                              VIN = l.VIN,
                                              Manufacturer = l.Manufacturer,
                                              Model = l.Model,
                                              Year = l.Year,
                                              Weight = l.Weight,
                                              Color = l.Color,
                                              EngineType = l.EngineType,
                                              TypeOfDrive = l.TypeOfdrive
                                          };

                                md.Entry(Lic).State = System.Data.Entity.EntityState.Modified;
                                md.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                GetMessage(ex.Message);
                            }
                        }
                    }));
            }
        }

        public int Con = 0;

        public DriverCardViewModel()
        {
            Licencess = new ObservableCollection<DriversAndLicences>();
            using (ModelDB md = new ModelDB())
            {
                var querry = from D in md.Drivers
                             from L in md.Licences
                             where D.ID == L.id
                             select new
                             {
                                 Name = D.Name,
                                 MiddleName = D.MiddleName,
                                 Adress = D.Adress,
                                 LicenceDate = L.LicenceDate,
                                 NameOrg = "null",
                                 Number = L.LicenceNumber,
                                 AdressLife = D.AdressLife,
                                 Categories = L.Categories,
                                 Photo = D.Photo,

                                 ExpDate = L.ExpireDate,

                                 LicenceSeries = L.LicenceSeries,
                                 VIN = L.VIN,
                                 Manufacturer = L.Manufacturer,
                                 Model = L.Model,
                                 Year = L.Year,
                                 Weight = L.Weight,
                                 Color = L.Color,
                                 EngineType = L.EngineType,
                                 TypeOfdrive = L.TypeOfDrive

                             };

                foreach (var s in querry)
                {
                    DriversAndLicences driversAndLicences = new DriversAndLicences
                    {
                        Name = s.Name,
                        MiddleName = s.MiddleName,
                        Adress = s.Adress,
                        AdressLife = s.AdressLife,
                        Categories = s.Categories,
                        Number = s.Number,
                        NameOrg = s.NameOrg,
                        DateOfIssure = s.LicenceDate,
                        Photo = $@"{AppDomain.CurrentDomain.BaseDirectory}/Assets/" + s.Photo,

                        LicenceSeries = s.LicenceSeries,
                        VIN = s.VIN,
                        Manufacturer = s.Manufacturer,
                        Model = s.Model,
                        Year = s.Year,
                        Weight = s.Weight,
                        Color = s.Color,
                        EngineType = s.EngineType,
                        TypeOfdrive = s.TypeOfdrive,
                        ExpireDate = s.ExpDate
                    };

                    if (s.ExpDate < DateTime.Now && s.LicenceDate.Value.Year != s.ExpDate.Value.Year) GetDriverInfo(driversAndLicences, "Утратил силу", "white");
                    else if (s.ExpDate.Value.Year == s.LicenceDate.Value.Year) GetDriverInfo(driversAndLicences, "Изъят", "red");
                    else GetDriverInfo(driversAndLicences, "Активен", "green");

                    Licencess.Add(driversAndLicences);
                    CountObject();

                }

            }
        }

        public void GetDriverInfo(DriversAndLicences driversAndLicences, string statinf, string nameInd)
        {
            driversAndLicences.Status = statinf;
            driversAndLicences.Ind = $@"{AppDomain.CurrentDomain.BaseDirectory}/Assets/{nameInd}C.png";
        }

        public DriverCardViewModel(List<DriversAndLicences> list)
        {

            Licencess = new ObservableCollection<DriversAndLicences>();
            Licencess.Clear();

            int count = list.Count;

            for (int i = 0; i < count; i++)
            {
                Licencess.Add(list[i]);
            }

        }

        public int CountObject()
        {
            Con++;
            return Con;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
