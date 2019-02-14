using ConversationManager.SeamButBetter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
namespace ConversationManager
{
    public class SBB : ISBB
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


        public void Update<T>(int id, T obj)
        {
            driver.Update(id, obj);          
        }

        public void CheckTimeout()
        {
            driver.CheckTimeout();
        }

    }
}
