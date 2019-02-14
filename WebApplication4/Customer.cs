using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ConversationManager
{
    [Serializable]
    public class Customer
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public List<Product> Products { get; set; }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public Customer Deserialize(string obj)
        {

            return (Customer)JsonConvert.DeserializeObject(obj, typeof(Customer));
        }


    }
}