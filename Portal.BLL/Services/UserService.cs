using AutoMapper;
using Microsoft.Win32;
using Portal.BLL.DTO;
using Portal.BLL.Infrastructure;
using Portal.BLL.Interfaces;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace Portal.BLL.Services
{
    public class UserService: IUserService
    {
        IUnitOfWork Database { get; set; }
        public UserService(IUnitOfWork iuof) 
        {
            Database = iuof;
        }
        public async Task CreateUser(UserDTO userDTO)
        {
            var user = new User
            {
                Name = userDTO.Name,
                LoginMail = userDTO.LoginMail,
                StatusAdmin = userDTO.StatusAdmin,
                Register = userDTO.Register,
                DateReg = DateTime.Now.ToString()
            };
            if (userDTO.StatusAdmin)
            {
                user.Register = true;
            }
            EncodingPassword(userDTO, user);
            await Database.Users.Create(user);
            await Database.Save();
        } 
        public async Task UpdateUser(UserDTO userDTO, bool confirm = false)
        {
            var user = new User
            {
                Id = userDTO.Id,
                Name = userDTO.Name,
                LoginMail = userDTO.LoginMail,
                Register = userDTO.Register,
                StatusAdmin = userDTO.StatusAdmin,
                DateReg = userDTO.DateReg
            };
            if (!confirm)
            {
                EncodingPassword(userDTO, user);
            }
            else
            {
                if (userDTO.Password != null)
                {
                    user.Password = userDTO.Password;
                }
                if (userDTO.Salt != null)
                {
                    user.Salt = userDTO.Salt;
                }
                if (userDTO.DateReg != null)
                {
                    user.DateReg = userDTO.DateReg;
                }
                user.StatusAdmin = userDTO.StatusAdmin;                
            }
            Database.Users.Update(user);
            
          await Database.Save();
        }
        public async Task DeleteUser(int id)
        {
            await Database.Users.Delete(id);
            await Database.Save();
        }
        public async Task<UserDTO> GetUserById(int id)
        {
            var user = await Database.Users.GetUserId(id);
            if (user == null)
                throw new ValidationExcept("Пользователь не найден!", "");
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                LoginMail = user.LoginMail,
                StatusAdmin = user.StatusAdmin,
                Password = user.Password,
                Salt = user.Salt,
                DateReg = user.DateReg,
                Register = user.Register
            };
        }
        public async Task<UserDTO> GetUserByLog(string log)
        {
            User user = await Database.Users.GetUserLog(log);
            if (user != null)
            {
                UserDTO udto = new UserDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    LoginMail = user.LoginMail,
                    StatusAdmin = user.StatusAdmin,
                    Password = user.Password,
                    Salt = user.Salt,
                    DateReg = user.DateReg,
                    Register = user.Register
                };
                return udto;
            }
            else
            {
                UserDTO udto = null;
                return udto;
            }
        }
        public async Task<bool> GetAdmin()
        {
            if (await Database.Users.GetAdmin())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(await Database.Users.GetAll());
        }
        public void EncodingPassword(UserDTO usdto, User us) //шифрование пароля
        {
            byte[] saltbuf = new byte[16];

            RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(saltbuf);

            StringBuilder sb = new StringBuilder(16);
            for (int i = 0; i < 16; i++)
                sb.Append(string.Format("{0:X2}", saltbuf[i]));
            string salt = sb.ToString();

            //переводим пароль в байт-массив  
            byte[] password = Encoding.Unicode.GetBytes(salt + usdto.Password);

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = SHA256.HashData(password);

            StringBuilder hash = new StringBuilder(byteHash.Length);
            for (int i = 0; i < byteHash.Length; i++)
                hash.Append(string.Format("{0:X2}", byteHash[i]));
            //если us == null - обращается верхний слой для шифрования и дальнейшего подтверждения пароля
            if (us != null)
            {
                us.Password = hash.ToString();
                us.Salt = salt;
            }
            else
            {
                usdto.Password = hash.ToString();
            }
        }
        public async Task<IEnumerable<UserDTO>> GetUsersRegister()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(await Database.Users.GetUsersRegister());
        }
    }
}
