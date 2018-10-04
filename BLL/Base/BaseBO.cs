using ClockStore.DTO.DBContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClockStore.BLL.Base
{
    public class BaseBO<TDBModel> where TDBModel : class
    {
        private ClockStoreContext db;

        public BaseBO()
        {
            db = new ClockStoreContext();
        }
        
        public virtual bool Insert(TDBModel obj)
        {
            db.Set<TDBModel>().Add(obj);
            return db.SaveChanges() > 0;
        }

        public virtual bool Update(TDBModel obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            return db.SaveChanges() > 0;
        }

        public virtual bool Delete(TDBModel obj)
        {
            db.Entry(obj).State = EntityState.Deleted;
            db.Set<TDBModel>().Remove(obj);
            return db.SaveChanges() > 0;
        }

        public virtual bool Delete<T>(T id)
        {
            var obj = db.Set<TDBModel>().Find(id);
            db.Set<TDBModel>().Remove(obj);
            return db.SaveChanges() > 0;
        }

        public virtual TDBModel Get<T>(T id)
        {
            return db.Set<TDBModel>().Find(id);
        }

        public virtual IEnumerable<TDBModel> GetAll()
        {
            return db.Set<TDBModel>().ToList();
        }

        public virtual void Dispose()
        {
            db.Dispose();
        }

         
    }
}
