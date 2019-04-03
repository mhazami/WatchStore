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

        public virtual List<TDBModel> GetAll()
        {
            return db.Set<TDBModel>().ToList();
        }

        public virtual TDBModel Get<T>(T id)
        {
            return db.Set<TDBModel>().Find(id);
        }

        public virtual void CheckConstraint(TDBModel obj)
        {
        }

        public virtual bool Insert(TDBModel obj)
        {
            this.CheckConstraint(obj);
            db.Set<TDBModel>().Add(obj);
            return db.SaveChanges() > 0;
        }

        public virtual bool Update(TDBModel obj)
        {
            this.CheckConstraint(obj);
            db.Entry(obj).State = EntityState.Modified;
            return db.SaveChanges() > 0;
        }

        public virtual void Dispose()
        {
            db.Dispose();
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
        public virtual List<TDBModel> Where(Expression<Func<TDBModel, bool>> query)
        {
            return db.Set<TDBModel>().Where(query).ToList();
        }

        public virtual TDBModel FirstOrDefault(Expression<Func<TDBModel, bool>> query)
        {
            return db.Set<TDBModel>().FirstOrDefault(query);
        }

        public virtual int Count()
        {
            return db.Set<TDBModel>().Count();
        }

        public virtual int Count(Expression<Func<TDBModel, bool>> query)
        {
            return db.Set<TDBModel>().Count(query);
        }

    }

    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }

    public class PredicateBuilder<T>
    {
        private readonly Dictionary<Expression<Func<T, bool>>, Operator> list = new Dictionary<Expression<Func<T, bool>>, Operator>();

        public PredicateBuilder<T> And(Expression<Func<T, bool>> func)
        {
            this.list.Add(func, Operator.And);
            return this;
        }

        public PredicateBuilder<T> Or(Expression<Func<T, bool>> func)
        {
            this.list.Add(func, Operator.Or);
            return this;
        }

        public IEnumerable<T> Where(IEnumerable<T> source)
        {
            var expression = GetExpression();
            return source.Where(expression.Compile());
        }

        public int Count(IEnumerable<T> source)
        {
            var expression = GetExpression();
            return source.Count(expression.Compile());
        }

        public T FirstOrDefault(IEnumerable<T> source)
        {
            var expression = GetExpression();
            return source.FirstOrDefault(expression.Compile());
        }

        public Expression<Func<T, bool>> GetExpression()
        {
            var expression = list.ContainsValue(Operator.Or) ? PredicateBuilder.False<T>() : PredicateBuilder.True<T>();

            foreach (var item in list)
            {
                switch (item.Value)
                {
                    case Operator.And:
                        expression = expression.And(item.Key);
                        break;
                    case Operator.Or:
                        expression = expression.Or(item.Key);
                        break;
                }
            }
            return expression;
        }

        public enum Operator
        {
            And,
            Or
        }

    }
}
