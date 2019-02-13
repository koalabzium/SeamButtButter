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
        
        public IDriver driver;

        public static SBB Instance(IDriver d)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new SBB(d);
                }
                return instance;
            }
        }

        private SBB(IDriver _driver)
        {          
            driver = _driver;
        }
        

        public void Add<T>(int id, T obj)
        {
            driver.Add<T>(id, obj);
        }


        public string Get(int id)
        {
            return driver.Get(id);
        }


        public void Delete (int id)
        {
            driver.Delete(id);
        }

        public void DeleteAll ()
        {

        }

        public void Update<T>(int id, T obj)
        {
            driver.Update(id, obj);          
        }

        public void CheckTimeout()
        {
            driver.CheckTimeout();
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
