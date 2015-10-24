using System;
using System.Linq;
using System.Linq.Expressions;

namespace RandomActs.Models
{
    public interface IRandomActorRepository
    {
        IQueryable<RandomActor> All { get; }
        IQueryable<RandomActor> AllIncluding(params Expression<Func<RandomActor, object>>[] includeProperties);
        RandomActor Find(int id);
        void InsertOrUpdate(RandomActor randomactor);
        void Delete(int id);
        void Save();
    }
}