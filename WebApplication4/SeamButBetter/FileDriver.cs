using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebApplication4
{
    public class FileDriver : IDriver
    {

        public int DefaultTimeout;
        public ContextList ContextList { get; set; }
        public string Path { get; set; }
        public int TimeOut { get; set; }

        public FileDriver(string path, int _TimeOut = 0)
        {
            Path = path;
            DefaultTimeout = _TimeOut;
        }


        public void Add<T>(int id, T obj)
        {

            var json = JsonConvert.SerializeObject(obj);

            string text = File.ReadAllText(Path);

            text = Regex.Unescape(text);

            var exists = false;

            DateTime now = DateTime.Now;

            Context tmp = new Context(id, json, DefaultTimeout, now);
            Context toDelete = new Context();
            if (text.Length > 0)
            {
                text = text.Remove(0, 1);
                text = text.Remove(text.Length - 1, 1);

                ContextList = (ContextList)JsonConvert.DeserializeObject(text, typeof(ContextList));

                foreach (var c in ContextList.Contexts)
                {
                    if (c.Id == tmp.Id)
                    {
                        exists = true;

                        if (DateTime.Now.Subtract(c.CreationTime).TotalMinutes >= DefaultTimeout)
                        {
                            toDelete = c;
                            exists = false;
                        }


                    }
                }

            }

            Delete(toDelete.Id);

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
            CheckTimeout();
            string text = File.ReadAllText(Path);
            text = Regex.Unescape(text);
            if (text.Length > 0)
            {
                text = text.Remove(0, 1);
                text = text.Remove(text.Length - 1, 1);

                ContextList = (ContextList)JsonConvert.DeserializeObject(text, typeof(ContextList));



                foreach (var c in ContextList.Contexts)
                {
                    if (c.Id == id)
                    {
                        var something = c.Values;
                        return something;

                    }
                }

            }

            return (string)Convert.ChangeType(null, typeof(string));
        }


        public void Delete(int id)
        {
            string text = File.ReadAllText(Path);

            text = Regex.Unescape(text);
            Context toDelete = new Context();

            if (text.Length > 0)
            {
                text = text.Remove(0, 1);
                text = text.Remove(text.Length - 1, 1);

                ContextList = (ContextList)JsonConvert.DeserializeObject(text, typeof(ContextList));

                foreach (var c in ContextList.Contexts)
                {
                    if (c.Id == id)
                    {
                        toDelete = c;
                    }
                }
                ContextList.Contexts.Remove(toDelete);

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

        public void DeleteAll()
        {

        }

        public void Update<T>(int id, T obj)
        {
            Delete(id);

            Add(id, obj);

        }

        public void CheckTimeout()
        {
            List<Context> toRemove = new List<Context>();
            string text = File.ReadAllText(Path);
            text = Regex.Unescape(text);
            if (text.Length > 0)
            {
                text = text.Remove(0, 1);
                text = text.Remove(text.Length - 1, 1);
                ContextList = (ContextList)JsonConvert.DeserializeObject(text, typeof(ContextList));
            }

            foreach (Context c in ContextList.Contexts)
            {

                if (c.TimeOut > 0)
                {
                    if (DateTime.Now.Subtract(c.CreationTime).TotalMinutes >= DefaultTimeout)
                    {
                        toRemove.Add(c);
                    }
                }
            }

            foreach (Context c in toRemove)
            {
                Delete(c.Id);
            }

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

