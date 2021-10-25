using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SklepInternetowy.Classes
{
    
    class Brand
    {
        
        private int id_Brand;
        private string nameBrand;

        [DisplayName("Id Marki")]
        public int Id_Brand
        {
            get => id_Brand;
            set => id_Brand = value;
        }

        [DisplayName("Nazwa Marki")]
        public string NameBrand 
        {
            get => nameBrand;
            set => nameBrand = value;
        }

      

        public Brand(object tempId_Brand, object tempNameBrand ) 
        {
            id_Brand =(int)tempId_Brand;
            nameBrand = tempNameBrand.ToString();
        }

    }
}