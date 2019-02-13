using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.SeamButBetter
{
    public class DbDriver : IDriver
    {
        private readonly TableDataGateway tdg;
        public ContextList ContextList { set; get; }


        public DbDriver(AppDbContext db)
        {
            tdg = new TableDataGateway(db);
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
            
        }

        public string Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Update<T>(int id, T obj)
        {
            throw new NotImplementedException();
        }
    }
}
