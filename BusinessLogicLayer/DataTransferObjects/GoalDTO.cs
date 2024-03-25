using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;


namespace BusinessLogicLayer.DataTransferObjects
{
    public class GoalDTO
    {
        public GoalDTO() { }

        public GoalDTO(Goal goal)
        {
            Id = goal.Id;
            Name = goal.Name;
            Amount = goal.Amount;
            Balance = goal.Balance;
            DateOfCreation = goal.DateOfCreation;
            DateToSaveUp = goal.DateToSaveUp;
            UserId = goal.UserId;
            if (DateToSaveUp < DateTime.Now)
                IsExpired = true;
            else IsExpired = false;
            if (Balance >= Amount)
                IsReached = true;
            else IsReached = false;
        }
        public GoalDTO(Goal goal, bool isReached, bool isExpired) 
        {
            Id = goal.Id;
            Name = goal.Name;
            Amount = goal.Amount;
            Balance = goal.Balance;
            DateOfCreation = goal.DateOfCreation;
            DateToSaveUp = goal.DateToSaveUp;
            UserId = goal.UserId;
            IsReached = isReached;
            IsExpired = isExpired;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public double Balance { get; set; }
        public bool IsReached { get; set; }
        public bool IsExpired { get; set; }
        public DateTime DateToSaveUp { get; set; }
        public DateTime DateOfCreation { get; set; }
        public int UserId { get; set;}
    }
}
