using System;
using Microsoft.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace RandomActs.Models
{ 
    public class RandomActActorRepository : IRandomActActorRepository
    {
        RAOKContext context = new RAOKContext();

        public IQueryable<RandomActActor> All
        {
            get { return context.RandomActActors; }
        }

        public IQueryable<RandomActActor> AllIncluding(params Expression<Func<RandomActActor, object>>[] includeProperties)
        {
            IQueryable<RandomActActor> query = context.RandomActActors;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public bool IsActorRegisteredAlready(int RandomActId, int RandomActorId)
        {
            return (context.RandomActActors.Where(x => x.RandomActId == RandomActId && x.RandomActorId == RandomActorId).Count() > 0);
        }

        public IQueryable<RandomActActor> FilterByActId(int actId)
        {
            return context.RandomActActors.Where(x => x.RandomActId == actId).Include(x => x.Actor).Include(x => x.Act);
        }

        public RandomActActor Find(int id) 
        {
            return context.RandomActActors.Include(x => x.Actor).Include(x => x.Act).SingleOrDefault(x => x.RandomActActorId == id);
        }

        public void InsertOrUpdate(RandomActActor randomactactor)
        {
            if (randomactactor.RandomActActorId == default(int)) {
                // New entity
                context.RandomActActors.Add(randomactactor);
            } else {
                // Existing entity
                context.Entry(randomactactor).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var randomactactor = Find(id);
            context.RandomActActors.Remove(randomactactor);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}