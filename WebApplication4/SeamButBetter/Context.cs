using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4
{
    public class Context
    {
        public int Id { get; set; }
        public string Values { get; set; }
        public DateTime CreationTime;
        public int TimeOut { get; set; }

        public Context(int Id, string Json)
        {
            this.Id = Id;
            Values = Json;
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
    }
}
