using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class GoalRepository : IRepository<Goal>
    {
        private DatabaseContext database;

        public GoalRepository(DatabaseContext database)
        {
            this.database = database;
        }

        public void Create(Goal goal)
        {
            database.Goals.Add(goal);
        }

        public void Update(Goal goal)
        {
            database.Entry(goal).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void Delete(int id)
        {
            Goal goal = database.Goals.Find(id);
            database.Goals.Remove(goal);
        }

        public IEnumerable<Goal> GetAll()
        {
            return database.Goals;
        }

        public Goal GetItem(int id)
        {
            return database.Goals.Find(id);
        }
    }
}
