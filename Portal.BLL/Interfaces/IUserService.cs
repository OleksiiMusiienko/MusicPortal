using Portal.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.BLL.Interfaces
{
    public interface IUserService
    {
        Task CreateUser(UserDTO userDTO);
        Task UpdateUser(UserDTO userDTO, bool confirm = false);
        Task DeleteUser(int id);
        Task<UserDTO> GetUserById(int id);
        Task<bool> GetAdmin();
        Task<UserDTO> GetUserByLog(string log);
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<IEnumerable<UserDTO>> GetUsersRegister();
    }
}
