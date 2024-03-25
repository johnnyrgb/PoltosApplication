using BusinessLogicLayer.DataTransferObjects;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUserService
    {
        UserDTO GetUser(int id);
        List<UserDTO> GetUsers();
        void CreateUser(UserDTO userDTO);
        void UpdateUser(UserDTO userDTO);
        void DeleteUser(int id);
    }
}
