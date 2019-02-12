using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
            string text = File.ReadAllText(Path);

            text = Regex.Unescape(text);

            var exists = false;

            Context tmp = new Context(id, json);

            if (text.Length > 0)
            {
                text = text.Remove(0, 1);
                text = text.Remove(text.Length - 1, 1);

                ContextList = (ContextList)JsonConvert.DeserializeObject(text, typeof(ContextList));

                foreach (var c in ContextList.Contexts)
                {
                    if (c.ContextId == tmp.ContextId)
                    {
                        exists = true;
                    }
                }

            }


            if (!exists)
            {
                ContextList.Append(tmp);
                var settings = new JsonSerializerSettings()
                {
                    StringEscapeHandling = StringEscapeHandling.EscapeNonAscii
                };


                var context = JsonConvert.SerializeObject(ContextList, settings);


                using (StreamWriter file = File.CreateText(Path))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, context);
                }

            }

        }

        public void Get(int id)
        {

        }
    }
}
