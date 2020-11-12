using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie1
{
    class DriversClass : INotifyPropertyChanged
    {
        private int guid;
        private string secname;
        private string name;
        private string lastname;
        private string passport;
        private string adressreg;
        private string adresslive;
        private string jobplace;
        private string position;
        private string phonenum;
        private string email;
        private string photo;
        private string description;

        public DriversClass(int guid, string secname, string name, string lastname, string passport, string adressreg, string adresslive, string jobplace, string position, string phonenum, string email, string photo, string description)
        {
            this.guid = guid;
            this.secname = secname;
            this.name = name;
            this.lastname = lastname;
            this.passport = passport;
            this.adressreg = adressreg;
            this.adresslive = adresslive;
            this.jobplace = jobplace;
            this.position = position;
            this.phonenum = phonenum;
            this.email = email;
            this.photo = photo;
            this.description = description;
        }

        public int Guid
        {
            get { return guid; }
            set
            {
                guid = value;
                OnPropertyChanged("Guid");
            }
        }

        public string Secname
        {
            get { return secname; }
            set
            {
                secname = value;
                OnPropertyChanged("Secname");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Lastname
        {
            get { return lastname; }
            set
            {
                lastname = value;
                OnPropertyChanged("Lastname");
            }
        }

        public string Passport
        {
            get { return passport; }
            set
            {
                passport = value;
                OnPropertyChanged("Passport");
            }
        }

        public string Adressreg
        {
            get { return adressreg; }
            set
            {
                adressreg = value;
                OnPropertyChanged("Adressreg");
            }
        }

        public string Adresslive
        {
            get { return adresslive; }
            set
            {
                adresslive = value;
                OnPropertyChanged("Adresslive");
            }
        }

        public string Jobplace
        {
            get { return jobplace; }
            set
            {
                jobplace = value;
                OnPropertyChanged("Jobplace");
            }
        }

        public string Position
        {
            get { return position; }
            set
            {
                position = value;
                OnPropertyChanged("Position");
            }
        }

        public string Phonenum
        {
            get { return phonenum; }
            set
            {
                phonenum = value;
                OnPropertyChanged("Phonenum");
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        public string Photo
        {
            get { return photo; }
            set
            {
                photo = value;
                OnPropertyChanged("Photo");
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
