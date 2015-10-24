using System;
using System.Linq;
using System.Linq.Expressions;

namespace RandomActs.Models
{
    public interface IRandomActRepository
    {
        IQueryable<RandomAct> All { get; }
        IQueryable<RandomAct> AllIncluding(params Expression<Func<RandomAct, object>>[] includeProperties);
        RandomAct Find(int id);
        void InsertOrUpdate(RandomAct randomact);
        void Delete(int id);
        void Save();
    }
}