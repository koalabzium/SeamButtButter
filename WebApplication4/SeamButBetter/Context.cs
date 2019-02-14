using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConversationManager.SeamButBetter
{
    public class Context : ValueObject
    {
        [JsonProperty]
        private int Id;

        [JsonProperty]
        private IDriver Driver;

        public string Values;

        public DateTime CreationTime;

        public int TimeOut { get; set; }

        public Context(int Id, string Json, IDriver driver)
        {
            this.Id = Id;
            Values = Json;
            Driver = driver;
        }

        public Context(int Id, string Json, int _TimeOut, DateTime _timeNow)
        {
            this.Id = Id;
            Values = Json;
            TimeOut = _TimeOut;
            CreationTime = _timeNow;
        }

        public Context()
        {

        }


        

        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return Id;
            yield return Driver;

        }

        public override int GetHashCode()
        {
            return GetAtomicValues()
             .Select(x => x != null ? x.GetHashCode() : 0)
             .Aggregate((x, y) => x ^ y);
        }


        public int GetId()
        {
            return Id;
        }

        public IDriver GetDriver()
        {
            return Driver;
        }

        public string GetValues()
        {
            return Values;
        }

    }
}
