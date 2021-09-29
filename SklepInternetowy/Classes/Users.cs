using System;
using System.Collections.Generic;
using System.Text;

namespace SklepInternetowy
{
    class Users
    {
        private string name;
        private string surname;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

       
        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }



        Users() 
        {

        }

        public Users(string Name,string Surname) 
        {
            name = Name;
            surname = Surname;
        }


    }
}
