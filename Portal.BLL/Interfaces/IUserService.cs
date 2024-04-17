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
        Task UpdateUser(UserDTO userDTO);
        Task DeleteUser(int id);
        Task<UserDTO> GetUserById(int id);
        Task<IEnumerable<UserDTO>> GetAllUsers();      
    }
}
