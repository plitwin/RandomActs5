using System;
using Microsoft.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace RandomActs.Models
{ 
    public class RandomActorRepository : IRandomActorRepository
    {
        RAOKContext context = new RAOKContext();

        public IQueryable<RandomActor> All
        {
            get { return context.RandomActors; }
        }

        public IQueryable<RandomActor> AllIncluding(params Expression<Func<RandomActor, object>>[] includeProperties)
        {
            IQueryable<RandomActor> query = context.RandomActors;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public RandomActor Find(int id)
        {
            return context.RandomActors.SingleOrDefault((x => x.RandomActorId == id));
        }

        public void InsertOrUpdate(RandomActor randomactor)
        {
            if (randomactor.RandomActorId == default(int)) {
                // New entity
                context.RandomActors.Add(randomactor);
            } else {
                // Existing entity
                context.Entry(randomactor).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var randomactor = Find(id);
            context.RandomActors.Remove(randomactor);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}