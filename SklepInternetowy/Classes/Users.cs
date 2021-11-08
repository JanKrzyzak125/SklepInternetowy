using SklepInternetowy.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SklepInternetowy
{
    class Users
    {
        private static Users logUser=null;
        private int id_User;
        private Int16 isActive;
        private string nick;
        private string name;
        private string surname;
        private string email;
        private int phone;
        private string adress;
        private string city;
        private int countVisitors;
        private Company company;
        private SQLConnect sqlConnect;

        public static Users LogUser 
        {
            get => logUser;
            
        }

        public int Id_User
        {
            get => id_User;
        }

        public Int16 IsActive
        {
            get => isActive;
            set => isActive = value;
        }

        public string Nick
        {
            get => nick;
            set => nick = value;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }


        public string Surname
        {
            get => surname;
            set => surname = value;
        }

        public string Email
        {
            get => email;
            set => email = value;
        }

        public int Phone
        {
            get => phone;
            set => phone = value;
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

        public int CountVisitors
        {
            get => countVisitors;
        }

        public Company Company
        {
            get => company;
            set => company = value;
        }


        public Users(object[] tempUser)
        {
            int valueIdUser=(int) tempUser[0];
            Int16 valueIsActive= (Int16)tempUser[1];
            string valueNick=(string)tempUser[2];
            string valueName= (string)tempUser[3];
            string valueSurname= (string)tempUser[4];
            string valueEmail= (string)tempUser[5];
            int valuePhone= (int)tempUser[6];
            string valueAdress= (string)tempUser[7];
            string valueCity= (string)tempUser[8];
            int valueCountVisitors= (int)tempUser[9];
            sqlConnect = new SQLConnect();

            id_User = valueIdUser;
            isActive = valueIsActive;
            nick = valueNick;
            name = valueName;
            surname = valueSurname;
            email = valueEmail;
            phone = valuePhone;
            adress = valueAdress;
            city = valueCity;
            countVisitors = valueCountVisitors;
           
            if (tempUser[10] is int)
            {
                Company tempCompany = new Company(sqlConnect.valueCompany((int)tempUser[10], "valueCompany"));
                company = tempCompany;
            }
            else 
            {
                company = null;
            }

            logUser = this;
        }

     
    }
}
