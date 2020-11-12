using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie1.Model
{
    class DriversAndLicences : INotifyPropertyChanged
    {
        public string name { get; set; }
        public string middleName { get; set; }
        public string Adress { get; set; }
        public DateTime? DateOfIssure { get; set; }
        public string AdressLife { get; set; }
        public string NameOrg { get; set; }
        public double? Number { get; set; }
        public string Categories { get; set; }
        public string photo { get; set; }

        public string Status { get; set; }
        public string Ind { get; set; }

        public string LicenceSeries { get; set; }
        public string VIN { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public double? Year { get; set; }
        public double? Weight { get; set; }
        public double? Color { get; set; }
        public double? EngineType { get; set; }
        public string TypeOfdrive { get; set; }
        public DateTime? ExpireDate { get; set; }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value; OnPropertyChanged("Name");
            }
        }

        public string MiddleName
        {
            get
            {
                return middleName;
            }
            set
            {
                middleName = value; OnPropertyChanged("MiddleName");
            }
        }

        public string Photo
        {
            get
            {
                return photo;
            }
            set
            {
                photo = value; OnPropertyChanged("Photo");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
