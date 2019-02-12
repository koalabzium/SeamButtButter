using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebApplication4
{
    public class SBB
    {
        private static SBB instance = null;
        private static readonly object padlock = new object();

        public string Path;
        public ContextList ContextList { get; set; }

        public static SBB Instance(string _path)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new SBB(_path);
                }
                return instance;
            }
        }


        private SBB(string path)
        {
            ContextList = new ContextList();
            Path = path;
        }

        public void Add<T>(int id, T obj)
        {

            var json = JsonConvert.SerializeObject(obj);

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


        public string Get(int id)
        {
            string toReturn = null;
            string text = File.ReadAllText(Path);
            text = Regex.Unescape(text);
            if (text.Length > 0)
            {
                text = text.Remove(0, 1);
                text = text.Remove(text.Length - 1, 1);

                ContextList = (ContextList)JsonConvert.DeserializeObject(text, typeof(ContextList));



                foreach (var c in ContextList.Contexts)
                {
                    if (c.ContextId == id)
                    {
                        var something = c.Values;
                        return something;

                    }
                }

            }

            return (string)Convert.ChangeType(null, typeof(string));
        }


        //public T Get<T>(int id)
        //{
        //    string toReturn = null;
        //    string text = File.ReadAllText(Path);
        //    text = Regex.Unescape(text);
        //    if (text.Length > 0)
        //    {
        //        text = text.Remove(0, 1);
        //        text = text.Remove(text.Length - 1, 1);

        //        ContextList = (ContextList)JsonConvert.DeserializeObject(text, typeof(ContextList));



        //        foreach (var c in ContextList.Contexts)
        //        {
        //            if (c.ContextId == id)
        //            {
        //                var something = c.Values;
        //                using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(something)))
        //                {
        //                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
        //                    return (T)serializer.ReadObject(ms);
        //                }

        //            }
        //        }

        //    }

        //    return (T)Convert.ChangeType(null, typeof(T));
        //}
    }
}
