using BusinessLogicLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IGoalService
    {
        List<GoalDTO> GetGoals();
        GoalDTO GetGoal(int id);
        void CreateGoal(GoalDTO goalDTO);
        void UpdateGoal(GoalDTO goalDTO);
        void DeleteGoal(int id);

        // custom
        public List<GoalDTO> GetGoalsByUserId(int userId);

    }
}
