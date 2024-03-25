using BusinessLogicLayer.DataTransferObjects;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private IDbRepository dbRepository;

        public UserService(IDbRepository dbRepository)
        {
            this.dbRepository = dbRepository;
        }
        public UserDTO GetUser(int id)
        {
            return new UserDTO(dbRepository.Users.GetItem(id));
        }
        public List<UserDTO> GetUsers()
        {
            return dbRepository.Users.GetAll().Select(item => new UserDTO(item)).ToList();
        }
        public void CreateUser(UserDTO userDTO)
        {
            dbRepository.Users.Create(new User()
            {
                Name = userDTO.Name,
                Password = userDTO.Password,
            });
            Save();
        }
        public void UpdateUser(UserDTO userDTO)
        {
            User user = dbRepository.Users.GetItem(userDTO.Id);
            user.Name = userDTO.Name;
            user.Password = userDTO.Password;
            Save();
        }
        public void DeleteUser(int id)
        {
            dbRepository.Users.Delete(id);
        }
        public bool Save()
        {
            return dbRepository.Save();
        }

    }
}
