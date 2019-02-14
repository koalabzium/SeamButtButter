using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConversationManager.SeamButBetter
{
    public class TableDataGateway
    {
        private readonly AppDbContext _db;
        public List<Context> ContextList { set; get; }
        public Context CurrentContext { set; get; }
        public int DefaultTimeout { get; set; }

        public TableDataGateway(AppDbContext db, int TimeOut)
        {
            _db = db;
            DefaultTimeout = TimeOut;
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
                    if (c.GetId() == CurrentContext.GetId())
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

        public async Task<int> CheckTimeout()
        {
            ContextList = await _db.Contexts.ToListAsync();
            foreach(var c in ContextList)
            {
                if (DateTime.Now.Subtract(c.CreationTime).TotalMinutes >= DefaultTimeout)
                {
                    await Delete(c.GetId());
                }
            }


            return 0;
        }

        public async Task<int> Delete(int id)
        {
            CurrentContext = await _db.Contexts.FindAsync(id);

            if (CurrentContext != null)
            {
                _db.Contexts.Remove(CurrentContext);
                await _db.SaveChangesAsync();
            }

            return id;
        }

        public async Task<string> Get(int id)
        {
            CurrentContext = await _db.Contexts.FindAsync(id);
   
            return CurrentContext.GetValues();
        }

        public async Task<int> Update<T>(int id, T obj)
        {
            await Delete(id);
            await AddAsync<T>(id, obj);          
            return id;
        }
    }
}

