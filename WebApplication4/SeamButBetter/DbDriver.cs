using System;
using System.Collections.Generic;
namespace ConversationManager.SeamButBetter
{
    public class DbDriver : IDriver
    {
        private readonly TableDataGateway tdg;
        public ContextList ContextList { set; get; }


        public DbDriver(AppDbContext db, int Timeout = 0)
        {
            tdg = new TableDataGateway(db, Timeout);
        }

        public void Add<T>(int id, T obj)
        {
            tdg.AddAsync<T>(id, obj);
        }

        public void CheckTimeout()
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            tdg.Delete(id);
        }

        public string Get(int id)
        {
            return Convert.ToString(tdg.Get(id));
        }

        public void Update<T>(int id, T obj)
        {
            tdg.Update(id, obj);
        }
    }
}
