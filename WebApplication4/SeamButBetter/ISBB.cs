using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConversationManager.SeamButBetter
{
    interface ISBB
    {
        void Add<T>(int id, T obj);
        string Get(int id);
        void Delete(int id);
        void Update<T>(int id, T obj);
        void CheckTimeout();
    }
}
