using BusinessLogicLayer.DataTransferObjects;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer.Services
{
    public class GoalService : IGoalService
    {
        private IDbRepository dbRepository;

        public GoalService(IDbRepository dbRepository)
        {
            this.dbRepository = dbRepository;
        }
        public List<GoalDTO> GetGoals()
        {
            return dbRepository.Goals.GetAll().Select(item => new GoalDTO(item)).ToList();
        }
        public GoalDTO GetGoal(int id)
        {
            return new GoalDTO(dbRepository.Goals.GetItem(id));
        }
        public void CreateGoal(GoalDTO goalDTO)
        {
            dbRepository.Goals.Create(new Goal()
            {
                Name = goalDTO.Name,
                Amount = goalDTO.Amount,
                DateOfCreation = goalDTO.DateOfCreation,
                DateToSaveUp = goalDTO.DateToSaveUp,
                UserId = goalDTO.UserId,
            });
            Save();
        }
        public void UpdateGoal(GoalDTO goalDTO)
        {
            Goal goal = dbRepository.Goals.GetItem(goalDTO.Id);
            goal.Name = goalDTO.Name;
            goal.Amount = goalDTO.Amount;
            goal.Balance = goalDTO.Balance;
            goal.DateOfCreation = goalDTO.DateOfCreation;
            goal.DateToSaveUp = goalDTO.DateToSaveUp;
            goal.UserId = goalDTO.UserId;
            Save();
        }
        public void DeleteGoal(int id)
        {
            dbRepository.Goals.Delete(id);
            Save();
        }

        //public void CheckGoalIsReachedById(int goalId, double moneyAmount)
        //{
        //    GoalDTO goal = GetGoal(goalId);
        //    if (goal.Amount <= moneyAmount)
        //    {
        //        goal.IsReached = true;
        //    }
        //}

        //public void CheckGoalIsExpiredById(int goalId, double moneyAmount)
        //{
        //    GoalDTO goal = GetGoal(goalId);
        //    if (goal.DateToSaveUp <= DateTime.Now)
        //    {
        //        goal.IsExpired = true;
        //    }
        //}

        //public void CheckGoalIsExpiredByItem(GoalDTO goalDTO, double moneyAmount)
        //{
         
        //    if (goalDTO.DateToSaveUp <= DateTime.Now)
        //    {
        //        goalDTO.IsExpired = true;
        //    }
        //}

        //public void CheckGoalIsReachedByItem(GoalDTO goalDTO, double moneyAmount)
        //{
        //    if (goalDTO.Amount <= moneyAmount)
        //    {
        //        goalDTO.IsReached = true;
        //    }
        //}

        public bool Save() 
        {
            return dbRepository.Save();
        }

        //custom
        public List<GoalDTO> GetGoalsByUserId(int userId)
        {
            return GetGoals().Where(goal => goal.UserId == userId).ToList();
        }
    }
}
