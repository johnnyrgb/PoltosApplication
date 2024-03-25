using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DataTransferObjects
{
    public class UserDTO
    {
        public UserDTO() { }
        public UserDTO(User user) 
        {
            Id = user.Id;
            Name = user.Name;
            Password = user.Password;
        }

        public UserDTO(User user, int accountCount)
        {
            Id = user.Id;
            Name = user.Name + " | Количество счетов: " + accountCount.ToString();
            Password = user.Password;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
