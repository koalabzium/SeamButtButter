using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4
{
    public class SBB
    {
        public string Path { get; set; }
        public Context Con { get; set; }

        public SBB(string path)
        {
            Path = path;
        }

        public void Add(int id, string json)
        {

            var context = JsonConvert.SerializeObject(new Context(id, json));



            using (StreamWriter file = File.CreateText(Path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, context);
            }


            Con = (Context)JsonConvert.DeserializeObject(context, typeof(Context));



        }
    }
}
