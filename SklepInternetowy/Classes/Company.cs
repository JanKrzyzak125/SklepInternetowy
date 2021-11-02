using System;
using System.Collections.Generic;
using System.Text;

namespace SklepInternetowy.Classes
{
    class Company
    {
        private int id_Company;
        private string nameCompany;
        private string adress;
        private string city;
        private int phone;
        private string email;
        private string nip;
        private int status;


        public int Id_Company
        {
            get => id_Company;
            set => id_Company = value;

        }

        public string NameCompany
        {
            get => nameCompany;
            set => nameCompany = value;
        }

        public string Adress
        {
            get => adress;
            set => adress = value;
        }

        public string City
        {
            get => city;
            set => city = value;
        }

        public int Phone
        {
            get => phone;
            set => phone = value;
        }

        public string Email
        {
            get => email;
            set => email = value;
        }

        public string NIP
        {
            get => nip;
            set => nip = value;
        }

        public int Status
        {
            get => status;
            set => status = value;
        }


        public Company(int valueId_Company, string valueNameCompany, string valueAdress,
                     string valueCity, int valuePhone, string valueEmail, string valueNIP,
                      int valueStatus)
        {
            id_Company = valueId_Company;
            nameCompany = valueNameCompany;
            adress = valueAdress;
            city = valueCity;
            phone = valuePhone;
            email = valueEmail;
            nip = valueNIP;
            status = valueStatus;
        }

        public Company(object[] tempCompany)
        {
            int tempId_Company = (int)tempCompany[0];
            string tempNameCompany = (string)tempCompany[1];
            string tempAdress = (string)tempCompany[2];
            string tempCity = (string)tempCompany[3];
            int tempPhone = (int)tempCompany[4];
            string tempEmail = (string)tempCompany[5];
            string tempNIP = (string)tempCompany[6];
            int tempStatus = (int)tempCompany[7];

            id_Company = tempId_Company;
            nameCompany = tempNameCompany;
            adress = tempAdress;
            city = tempCity;
            phone = tempPhone;
            email = tempEmail;
            nip = tempNIP;
            status = tempStatus;

        }
    }
}
