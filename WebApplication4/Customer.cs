using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebApplication4
{
    [Serializable]
    public class Customer
    {
        
        public int Id { get; set; } //prop tab tab
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Product> Products { get; set; }

        //[Required, StringLength(50)]
        //public string Name { get; set; }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public Customer Deserialize(string obj)
        {

            return (Customer) JsonConvert.DeserializeObject(obj, typeof(Customer));
        }


    }
}