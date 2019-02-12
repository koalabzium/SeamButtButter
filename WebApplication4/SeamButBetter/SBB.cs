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
        private static SBB instance = null;
        private static readonly object padlock = new object();

        public string Path = "NaszDzejsonek.json";
        public ContextList ContextList { get; set; }

        public static SBB Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new SBB();
                    }
                    return instance;
                }
            }
        }


        private SBB()
        {
            ContextList = new ContextList();
            
        }

        public void Add<T>(int id, T obj)
        {

            var json = JsonConvert.SerializeObject(obj);

            //using (StreamReader file = File.OpenText(Path))
            //{
            //    JsonSerializer serializer = new JsonSerializer();
            //    ContextList = (ContextList)serializer.Deserialize(file, typeof(ContextList));
            //}


            Context tmp = new Context(id, json);

         
            ContextList.Append(tmp);


            var context = JsonConvert.SerializeObject(ContextList);

            using (StreamWriter file = File.CreateText(Path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, context);
            }

            ContextList = (ContextList)JsonConvert.DeserializeObject(context, typeof(ContextList));

            //TODO dodać sprawdzanie czy już taki jest!!!!!!!!!!!!!!!!!!!!!!!!!!!!


        }

        public void Get(int id)
        {

        }
    }
}
