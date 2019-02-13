using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4
{
    public class Context
    {
        public int ContextId { get; set; }
        public string Values { get; set; }
        public readonly DateTime CreationTime = DateTime.Now;
        public int TimeOut { get; set; }

        public Context(int Id, string Json)
        {
            ContextId = Id;
            Values = Json;
        }

        public Context(int Id, string Json, int _TimeOut)
        {
            ContextId = Id;
            Values = Json;
            TimeOut = _TimeOut;
        }

        public Context()
        {

        }
    }
}
