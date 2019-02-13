using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.SeamButBetter
{
    public class TableDataGateway
    {
        private readonly AppDbContext _db;
        public List<Context> ContextList { set; get; }
        public Context CurrentContext { set; get; }

        public TableDataGateway(AppDbContext db)
        {
            _db = db;
        }

        public async Task<T> AddAsync<T>(int id, T obj)
        {
            var exists = false;
            ContextList = await _db.Contexts.ToListAsync();
            CurrentContext = await _db.Contexts.FindAsync(id);

            if (CurrentContext != null)
            {
                foreach (var c in ContextList)
                {
                    if (c.Id == CurrentContext.Id)
                    {
                        exists = true;
                        break;
                    }
                }
            }

            if (!exists)
            {
                _db.Contexts.Add(CurrentContext);
                await _db.SaveChangesAsync();
            }

            return obj;

        }

        public void CheckTimeout()
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            //var Curre = await _db.Customers.FindAsync(id);

            //if (customer != null)
            //{
            //    _db.Customers.Remove(customer);
            //    await _db.SaveChangesAsync();
            //}
            throw new NotImplementedException();
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

